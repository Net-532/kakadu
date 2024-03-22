using System.Net;
using System.Net.Sockets;
using System.Security;
using System.Text;

namespace WebServer
{
    public class Program
    {
        private static TcpListener server = null;
        
        private static Int32 port = 8085;
        private static IPAddress address = IPAddress.Parse("127.0.0.1");
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

                    //stream.Close();
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