using System;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace CoffeeClient
{
    /// <summary>
    /// Coffee Shop Chatbot Client
    /// Windows Forms application that connects to the coffee shop server
    /// and allows users to place orders via a chat interface.
    /// </summary>
    public partial class Form1 : Form
    {
        private TcpClient client;
        private NetworkStream networkStream;
        private StreamReader reader;
        private StreamWriter writer;
        private Thread listenerThread;
        private const string ServerIP = "127.0.0.1";
        private const int ServerPort = 9000;

        public Form1()
        {
            InitializeComponent();
            ConnectToServer();
        }

        /// <summary>
        /// Establishes connection to the coffee shop server.
        /// Sets up network streams and starts the background listener thread.
        /// </summary>
        private void ConnectToServer()
        {
            try
            {
                // Connect to server
                client = new TcpClient(ServerIP, ServerPort);
                networkStream = client.GetStream();
                reader = new StreamReader(networkStream);
                writer = new StreamWriter(networkStream) { AutoFlush = true };

                // Display welcome message from server
                AppendMessage("[SYSTEM] Connected to Coffee Server");
                string welcomeMsg = reader.ReadLine();
                if (welcomeMsg != null)
                    AppendMessage(welcomeMsg);

                // Display menu from server
                string menuMsg = reader.ReadLine();
                if (menuMsg != null)
                    AppendMessage(menuMsg);

                // Start background thread to listen for server messages
                listenerThread = new Thread(ListenForServerMessages);
                listenerThread.IsBackground = true;
                listenerThread.Start();
            }
            catch (SocketException)
            {
                AppendMessage("[ERROR] Unable to connect to server. Make sure the server is running.");
            }
            catch (Exception ex)
            {
                AppendMessage($"[ERROR] Connection failed: {ex.Message}");
            }
        }

        /// <summary>
        /// Handles the Send button click event.
        /// Sends the user's message to the server and displays it in the chat.
        /// </summary>
        private void btnSend_Click(object sender, EventArgs e)
        {
            string message = txtInput.Text.Trim();
            if (string.IsNullOrEmpty(message)) return;

            try
            {
                // Send message to server
                writer.WriteLine(message);
                AppendMessage($"You: {message}");
                txtInput.Clear();
                txtInput.Focus();
            }
            catch (IOException)
            {
                AppendMessage("[ERROR] Unable to send message. Server may have disconnected.");
            }
        }

        /// <summary>
        /// Background thread method that continuously listens for messages from the server.
        /// Updates the chat display on the UI thread when messages arrive.
        /// </summary>
        private void ListenForServerMessages()
        {
            try
            {
                while (true)
                {
                    string serverMessage = reader.ReadLine();

                    // Server disconnected
                    if (serverMessage == null)
                        break;

                    // Update UI on the UI thread (thread-safe)
                    Invoke(new Action(() => AppendMessage(serverMessage)));
                }
            }
            catch (IOException)
            {
                // Server disconnected unexpectedly
                if (!this.IsDisposed)
                {
                    Invoke(new Action(() => AppendMessage("[WARNING] Connection to server lost.")));
                }
            }
        }

        /// <summary>
        /// Appends a message to the chat display textbox.
        /// </summary>
        /// <param name="message">Message to display</param>
        private void AppendMessage(string message)
        {
            txtChat.AppendText(message + Environment.NewLine);
        }

        /// <summary>
        /// Handles the Enter key press in the input textbox.
        /// Sends the message when Enter is pressed.
        /// </summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter && txtInput.Focused)
            {
                btnSend_Click(this, EventArgs.Empty);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// Cleans up resources when the form is closed.
        /// Closes all network streams and the client connection.
        /// </summary>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // Stop the listener thread
                if (listenerThread != null && listenerThread.IsAlive)
                {
                    listenerThread.Abort();
                }

                // Close all streams and connection
                reader?.Close();
                writer?.Close();
                networkStream?.Close();
                client?.Close();
            }
            catch { }
        }
    }
}
