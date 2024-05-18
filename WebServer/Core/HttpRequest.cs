namespace Kakadu.WebServer.Core
{
    public class HttpRequest
    {
        public HttpMethod Method { get; set; }
        public string RootPath { get; set; }
        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, string> Parameters { get; set; } = new Dictionary<string, string>();
        public string Body { get; set; } = string.Empty;
    }
}
