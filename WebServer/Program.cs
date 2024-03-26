﻿using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Kakadu.WebServer
{
    public class Program
    {
        private static TcpListener server = null;
        
        private static readonly Int32 port = 8085;
        private static readonly IPAddress address = IPAddress.Parse("127.0.0.1");
        public static async Task Main()
        {
            try
            { 
                server = new TcpListener(address, port);
                server.Start();

                Console.WriteLine($"Web Server Running on {address.ToString()} on port {port}...");
                
                while (true)
                {
                    TcpClient client = await server.AcceptTcpClientAsync();
                    
                    NetworkStream stream = client.GetStream();

                    string response = "I am OK. Waiting for instructions";
                    byte[] responseData = Encoding.UTF8.GetBytes(response);
                    await stream.WriteAsync(responseData, 0, responseData.Length);

                    client.Close();
                    
                    Console.WriteLine("Response sent");
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                server.Stop();
            }
        }
    }
}