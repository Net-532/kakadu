using Kakadu.Backend.Entities;
using Kakadu.Backend.Repositories;
using Kakadu.Backend.Services;
using Microsoft.Extensions.Configuration;
using Serilog;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Kakadu.WebServer
{
    public class WebServer
    {
        private static readonly int port = 8085;
        private static readonly IPAddress address = IPAddress.Parse("127.0.0.1");
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

                    string request = Encoding.UTF8.GetString(buffer, 0, bytesReceived);

                    HttpRequest httpRequest = httpMessageConverter.Convert(request);
                    HttpResponse httpResponse = httpRequestDispatcher.Dispatch(httpRequest);

                    byte[] responseData = Encoding.UTF8.GetBytes(httpResponse.Body);
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
