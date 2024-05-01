using Kakadu.Backend.Entities;
using Kakadu.Backend.Repositories;
using Kakadu.Backend.Services;
using Microsoft.Extensions.Configuration;
using Serilog;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Kakadu.WebServer
{
    public class WebServer
    {
        private static readonly Int32 port = 8085;
        private static readonly IPAddress address = IPAddress.Parse("127.0.0.1");
        private static IProductService productService = new ProductService(new ProductRepositoryXML());
        private static IOrderService orderService = new OrderService();
        private static IOrderRepository orderRepository = new OrderRepositoryXML();

        public static void Main()
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(new ConfigurationBuilder()
                    .AddJsonFile("serilog.json")
                    .Build())
                .CreateLogger();

            TcpListener? server = null;
            try
            {
                server = new TcpListener(address, port);
                server.Start();

                Log.Information("Web Server Running on {Address} on port {Port}...", address, port);

                while (true)
                {
                    Socket clientSocket = server.AcceptSocket();

                    byte[] buffer = new byte[1024];
                    int bytesReceived = clientSocket.Receive(buffer);

                    string request = Encoding.UTF8.GetString(buffer, 0, bytesReceived);
                    string requestMethod = request.Split(' ')[0];

                    if (requestMethod == "GET" && request.Contains("/products"))
                    {
                        List<Product> products = productService.GetAll();

                        if (products != null)
                        {
                            StringBuilder jsonBuilder = new StringBuilder();
                            string response = "HTTP/1.1 200 OK\\r\\n\" + \"Content-Type: application/json\\r\\n\" + \"Access-Control-Allow-Origin: *\\r\\n\\r\\n\" + \"[\"";
                            foreach (Product product in products)
                            {
                                string priceString = product.Price.ToString();
                                priceString = priceString.Replace(",", ".");

                                string productJson = $"{{\"id\": {product.Id}, \"title\": \"{product.Title}\", \"price\": {priceString}, \"photoUrl\": \"{product.PhotoUrl}\", \"description\": \"{product.Description}\"}},";
                                jsonBuilder.Append(productJson);
                            }
                            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                            jsonBuilder.Append("]");

                            response += jsonBuilder.ToString();

                            Log.Information(response);

                            byte[] responseData = Encoding.UTF8.GetBytes(response);
                            clientSocket.Send(responseData);
                        }
                        Console.WriteLine("Response sent");
                    }
                    else if (requestMethod == "POST" && request.Contains("/orders"))
                    {
                        Order order = new Order();
                        orderRepository.Save(order);
                    }

                    clientSocket.Shutdown(SocketShutdown.Send);
                    clientSocket.Close();

                    Log.Information("Response sent");
                }
            }
            catch (SocketException e)
            {
                Log.Error(e, "SocketException occurred");
            }
            finally
            {
                server?.Stop();
            }
        }
    }
}
