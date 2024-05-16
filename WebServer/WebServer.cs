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
        private static readonly int port = 8085;
        private static readonly IPAddress address = IPAddress.Parse("127.0.0.1");
        private static IProductService productService = new ProductService(new ProductRepositoryXML());
        private static IOrderService orderService = new OrderService(orderRepository);
        private static IOrderRepository orderRepository = new OrderRepositoryXML();
        private static readonly HttpRequestDispatcher httpRequestDispatcher = new HttpRequestDispatcher();
        private static readonly HttpMessageConverter httpMessageConverter = new HttpMessageConverter();

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

                    HttpRequest httpRequest;
                    HttpResponse httpResponse;
                    string response;

                    string request = Encoding.UTF8.GetString(buffer, 0, bytesReceived);
                    if (string.IsNullOrEmpty(request))
                    {
                        httpResponse = new HttpResponse();
                        httpResponse.Status = HttpStatus.OK;
                        response = httpResponse.ToString();
                    }
                    else
                    {
                        httpRequest = httpMessageConverter.Convert(request);
                        httpResponse = httpRequestDispatcher.Dispatch(httpRequest);
                        response = httpResponse.ToString();
                    }

                    Log.Debug("Response is {0}", response);
                    byte[] responseData = Encoding.UTF8.GetBytes(response);
                    clientSocket.Send(responseData);
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