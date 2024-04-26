namespace Kakadu.WebServer
{
    public class HttpRequest

    {
        public string RootPath { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public string Body { get; set; }
        public Dictionary<string, string> Parameters { get; set; }
    }
}