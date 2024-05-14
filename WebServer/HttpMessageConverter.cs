using WebServer;

namespace Kakadu.WebServer
{
    public enum HttpMethod
    {
        GET,
        POST,
        PUT,
        DELETE,
        OPTIONS
    }

    public class HttpMessageConverter
    {
        public HttpRequest Convert(string httpMessage)
        {
            HttpRequest request = new HttpRequest();

            string[] lines = httpMessage.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

            string[] requestLine = lines[0].Split(' ');

            switch (requestLine[0])
            {
                case "GET":
                    request.Method = HttpMethod.GET;
                    break;
                case "POST":
                    request.Method = HttpMethod.POST;
                    break;
                case "PUT":
                    request.Method = HttpMethod.PUT;
                    break;
                case "DELETE":
                    request.Method = HttpMethod.DELETE;
                    break;
                case "OPTIONS":
                    request.Method = HttpMethod.OPTIONS;
                    break;
                default:
                    throw new NotSupportedHttpMethodException(requestLine[0]);
            }

            request.RootPath = requestLine[1];


            int emptyLineIndex = Array.IndexOf(lines, "");

            if (emptyLineIndex != -1 && emptyLineIndex < lines.Length - 1)
            {
                for (int i = 1; i < emptyLineIndex; i++)
                {
                    string[] header = lines[i].Split(new[] { ": " }, StringSplitOptions.None);
                    request.Headers.Add(header[0], header[1]);
                }

                request.Body = string.Join("\n", lines, emptyLineIndex + 1, lines.Length - emptyLineIndex - 1);
            }

            return request;
        }
    }
}
