using System.Text;

namespace Kakadu.WebServer
{
    public class HttpResponse
    {
        public HttpStatus Status { get; set; }
        public string Body { get; set; }
        public Dictionary<string, string> Headers { get; set; }

        public HttpResponse()
        {
            Headers = new Dictionary<string, string>();
        }
        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendLine($"HTTP/1.1 {(int)Status} {Status}");

            if (Headers != null)
            {
                foreach (var header in Headers)
                {
                    builder.AppendLine($"{header.Key}: {header.Value}");
                }
            }

            builder.AppendLine();

            if (!string.IsNullOrEmpty(Body))
            {
                builder.Append(Body);
            }

            return builder.ToString();
        }

    }
}
