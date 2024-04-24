using Kakadu.Backend.Entities;
using Kakadu.Backend.Repositories;
using Kakadu.Backend.Services;
using Serilog;
using Serilog.Core;
using System.Net.Sockets;
using System.Net;
using System.Text;

public class WebServer
{
    private static readonly Int32 port = 8085;
    private static readonly IPAddress address = IPAddress.Parse("127.0.0.1");
    private static IProductService productService = new ProductService(new ProductRepositoryXML());

    public static void Main()
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        TcpListener server = null;
        try
        {
            server = new TcpListener(address, port);
            server.Start();

            Log.Information($"Web Server Running on {address.ToString()} on port {port}...");

            while (true)
            {
                Socket clientSocket = server.AcceptSocket();

                List<Product> products = productService.GetAll();

                if (products != null)
                {
                    StringBuilder jsonBuilder = new StringBuilder();
                    string response = "HTTP/1.1 200 OK\r\n" + "Content-Type: application/json\r\n" + "Access-Control-Allow-Origin: *\r\n\r\n" + "[";
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
