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
        private static readonly IPAddress address = IPAddress.Any;
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

                Socket clientSocket = null;

                while (true)
                {
                    clientSocket = server.AcceptSocket();
                    byte[] buffer = new byte[1024];
                    int bytesReceived = clientSocket.Receive(buffer);
                    string request = Encoding.UTF8.GetString(buffer, 0, bytesReceived);

                    try
                    {
                        HttpRequest httpRequest = httpMessageConverter.Convert(request);
                        HttpResponse httpResponse = httpRequestDispatcher.Dispatch(httpRequest);
                        SendMessageByByte(clientSocket, httpResponse);
                    }
                    catch (Exception ex)
                    {
                        HttpResponse response = new HttpResponse();
                        response.Body = ex.Message;
                        response.Status = HttpStatus.BadRequest;
                        SendMessageByByte(clientSocket, response);
                        Log.Information("BadRequest");
                    }
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

        private static void SendMessageByByte(Socket clientSocket, HttpResponse response) {
            string responseStr = response.ToString();
            Log.Debug("Response is {0}", responseStr);
            byte[] responseData = Encoding.UTF8.GetBytes(responseStr);

            clientSocket.Send(responseData);
            clientSocket.Shutdown(SocketShutdown.Send);
            clientSocket.Close();
        }
    }
}