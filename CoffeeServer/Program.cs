using System;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace CoffeeServer
{
    /// <summary>
    /// Coffee Shop Chatbot Server
    /// Listens for client connections and processes coffee orders via TCP sockets.
    /// </summary>
    class Program
    {
        private const int Port = 9000;

        static void Main()
        {
            TcpListener server = new TcpListener(IPAddress.Any, Port);
            server.Start();

            Console.WriteLine("========================================");
            Console.WriteLine("   COFFEE SHOP SERVER");
            Console.WriteLine("========================================");
            Console.WriteLine($"Listening on port {Port}...");
            Console.WriteLine("Waiting for customers...\n");

            // Main server loop - accepts multiple clients sequentially
            while (true)
            {
                try
                {
                    // Wait for a client to connect
                    Socket clientSocket = server.AcceptSocket();
                    Console.WriteLine("[CONNECTED] New customer joined");

                    // Set up network communication streams
                    NetworkStream networkStream = new NetworkStream(clientSocket);
                    StreamReader reader = new StreamReader(networkStream);
                    StreamWriter writer = new StreamWriter(networkStream) { AutoFlush = true };

                    // Send welcome message and menu to newly connected client
                    writer.WriteLine(GetWelcomeMessage());
                    writer.WriteLine(GetMenu());

                    // Process client orders in a loop
                    HandleClientCommunication(reader, writer, networkStream, clientSocket);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] Server handled error: {ex.Message}\n");
                }
            }
        }

        /// <summary>
        /// Handles the communication loop with a connected client.
        /// Reads client messages, processes orders, and sends responses.
        /// </summary>
        static void HandleClientCommunication(StreamReader reader, StreamWriter writer,
                                               NetworkStream ns, Socket clientSocket)
        {
            try
            {
                while (true)
                {
                    string clientMessage = reader.ReadLine();

                    // Client disconnected
                    if (clientMessage == null)
                        break;

                    Console.WriteLine($"[ORDER] Client: {clientMessage}");

                    // Process the order and send response
                    string response = ProcessOrder(clientMessage.ToLower().Trim());
                    writer.WriteLine(response);
                }
            }
            catch (IOException)
            {
                Console.WriteLine("[WARNING] Client disconnected unexpectedly");
            }
            finally
            {
                Console.WriteLine("[DISCONNECTED] Customer left\n");
                writer.Close();
                reader.Close();
                ns.Close();
                clientSocket.Close();
            }
        }

        /// <summary>
        /// Returns the welcome message for new clients.
        /// </summary>
        static string GetWelcomeMessage()
        {
            return "========================================\n" +
                   "   Welcome to Coffee Shop!\n" +
                   "========================================\n" +
                   "Type a number (1-3) or coffee name to order.\n" +
                   "Type 'menu' to see options. Type 'exit' to quit.";
        }

        /// <summary>
        /// Returns the coffee menu with prices.
        /// </summary>
        static string GetMenu()
        {
            return "\n--- MENU ---\n" +
                   "1. Espresso   - Rs 300\n" +
                   "2. Cappuccino - Rs 350\n" +
                   "3. Latte      - Rs 400\n" +
                   "-------------\n" +
                   "Your order: ";
        }

        /// <summary>
        /// Processes the client's order message and returns an appropriate response.
        /// Supports both numeric selection (1-3) and coffee name input.
        /// </summary>
        /// <param name="input">Lowercase, trimmed client input</param>
        /// <response>Server response message</param>
        static string ProcessOrder(string input)
        {
            // Menu request
            if (input == "menu" || input == "help")
                return GetMenu();

            // Exit request
            if (input == "exit" || input == "quit" || input == "bye")
                return "Thank you for visiting Coffee Shop! Goodbye!";

            // Espresso
            if (input == "1" || input.Contains("espresso"))
                return "[ORDER CONFIRMED] Espresso - Rs 300\nYour order: ";

            // Cappuccino
            if (input == "2" || input.Contains("cappuccino"))
                return "[ORDER CONFIRMED] Cappuccino - Rs 350\nYour order: ";

            // Latte
            if (input == "3" || input.Contains("latte"))
                return "[ORDER CONFIRMED] Latte - Rs 400\nYour order: ";

            // Unknown input
            return "[UNKNOWN] Invalid option. Type 'menu' to see available options.\nYour order: ";
        }
    }
}
