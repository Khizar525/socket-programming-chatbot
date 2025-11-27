using System;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace CoffeeServer
{
    class Program
    {
        static void Main()
        {
            TcpListener server = new TcpListener(IPAddress.Any, 9000);
            server.Start();

            Console.WriteLine("☕ Coffee Shop Server Running...");
            Console.WriteLine("Waiting for customers...\n");

            while (true)
            {
                try
                {
                    Socket client = server.AcceptSocket();
                    Console.WriteLine("\n🔗 Client Connected\n");

                    NetworkStream ns = new NetworkStream(client);
                    StreamReader reader = new StreamReader(ns);
                    StreamWriter writer = new StreamWriter(ns) { AutoFlush = true };

                    // Send menu immediately
                    writer.WriteLine(Menu());

                    while (true)
                    {
                        string msg = reader.ReadLine();
                        if (msg == null) break; // client closed connection safely

                        Console.WriteLine($"Client Ordered: {msg}");
                        writer.WriteLine(Process(msg.ToLower()));
                    }

                    Console.WriteLine("❌ Client disconnected.\n");
                    ns.Close();
                    client.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("\n⚠ Server Handled Error (No Crash)");
                    Console.WriteLine(ex.Message + "\n");
                }
            }
        }

        static string Menu()
        {
            return "📋 MENU >> 1:Espresso(300) | 2:Cappuccino(350) | 3:Latte(400) | ";
        }

        static string Process(string m)
        {
            if (m == "menu") return Menu();
            if (m == "1" || m.Contains("espresso")) return "☕ Espresso Selected — Rs 300";
            if (m == "2" || m.Contains("cappuccino")) return "☕ Cappuccino Selected — Rs 350";
            if (m == "3" || m.Contains("latte")) return "☕ Latte Selected — Rs 400";
            return "❓ Unknown command — type 'menu' to see menu again.";
        }
    }
}
