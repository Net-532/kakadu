using Kakadu.Backend.Entities;
using Kakadu.Backend.Repositories;
using Kakadu.Backend.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Kakadu.WebServer
{
    public class WebServer
    {
        private static readonly Int32 port = 8085;
        private static readonly IPAddress address = IPAddress.Parse("127.0.0.1");
        private static IProductService productService;

        public static void Main()
        {
            TcpListener server = null;
            try
            {
                server = new TcpListener(address, port);
                server.Start();

                Console.WriteLine($"Web Server Running on {address.ToString()} on port {port}...");

                while (true)
                {
                    productService = new ProductService(new ProductRepositoryXML());

                    Socket clientSocket = server.AcceptSocket();


                    List<Product> products = productService.GetAll();

                    if (products != null)
                    {
                        StringBuilder jsonBuilder = new StringBuilder();
                        string response = "HTTP/1.1 200 OK\r\n" + "Content-Type: application/json\r\n" + "Access-Control-Allow-Origin: *\r\n\r\n" + "[";
                        foreach (Product product in products)
                        {
                            string productJson = $"{{\"Id\": {product.Id}, \"Title\": \"{product.Title}\", \"Price\": {product.Price}, \"PhotoUrl\": \"{product.PhotoUrl}\", \"Description\": \"{product.Description}\"}},";
                            jsonBuilder.Append(productJson);
                        }
                        jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                        jsonBuilder.Append("]}");

                        response += jsonBuilder.ToString();

                        Console.WriteLine(response);

                        byte[] responseData = Encoding.UTF8.GetBytes(response);
                        clientSocket.Send(responseData);
                    }

                    clientSocket.Shutdown(SocketShutdown.Send);
                    clientSocket.Close();

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
