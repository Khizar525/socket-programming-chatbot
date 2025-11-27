using System;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace CoffeeClient
{
    public partial class Form1 : Form
    {
        TcpClient client;
        NetworkStream ns;
        StreamReader sr;
        StreamWriter sw;
        Thread listenThread;

        public Form1()
        {
            InitializeComponent();
            ConnectToServer();
        }

        void ConnectToServer()
        {
            try
            {
                client = new TcpClient("127.0.0.1", 9000);
                ns = client.GetStream();
                sr = new StreamReader(ns);
                sw = new StreamWriter(ns) { AutoFlush = true };

                Append("☕ Connected to Coffee Server");
                Append(sr.ReadLine()); // First menu message

                listenThread = new Thread(Listen);
                listenThread.IsBackground = true;
                listenThread.Start();
            }
            catch
            {
                Append("❌ Server is not running.");
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (txtInput.Text.Trim() == "") return;

            sw.WriteLine(txtInput.Text);
            Append($"You: {txtInput.Text}");
            txtInput.Clear();
        }

        void Listen()
        {
            try
            {
                while (true)
                {
                    string msg = sr.ReadLine();
                    if (msg == null) break;

                    Invoke(new Action(() => Append(msg)));
                }
            }
            catch
            {
                Append("⚠ Connection lost.");
            }
        }

        void Append(string message)
        {
            txtChat.AppendText(message + Environment.NewLine);
        }
    }
}
