using Kakadu.Backend.Services;
using Kakadu.WebServer.Core;
using System;

namespace Kakadu.WebServer.OrderAPI
{
    public class SendMailRequestProcessor
    {
        private readonly OrderEmailService _orderEmailService;

        public SendMailRequestProcessor(OrderEmailService orderEmailService)
        {
            _orderEmailService = orderEmailService;
        }

        public HttpResponse Process(HttpRequest httpRequest)
        {
            HttpResponse response = new HttpResponse();
            try
            {
                var parameters = ParseJson(httpRequest.Body);
                string recipient = parameters["recipient"];
                int orderId = Convert.ToInt32(parameters["orderId"]);

                _orderEmailService.SendOrderbyEmail(orderId, recipient);

                response.Status = HttpStatus.OK;
                response.Body = "{\"status\": \"Email sent successfully\"}";
            }
            catch (Exception ex)
            {
                response.Status = HttpStatus.InternalServerError;
                response.Body = $"{{\"error\": \"{EscapeString(ex.Message)}\"}}";
            }

            return response;
        }

        private static Dictionary<string, string> ParseJson(string json)
        {
            var parameters = new Dictionary<string, string>();
            json = json.Trim().Trim('{', '}').Trim();

            var keyValuePairs = json.Split(',');

            foreach (var pair in keyValuePairs)
            {
                var keyValue = pair.Split(':');
                if (keyValue.Length != 2)
                {
                    throw new FormatException("Invalid JSON format");
                }

                var key = keyValue[0].Trim().Trim('"');
                var value = keyValue[1].Trim().Trim('"');

                parameters[key] = value;
            }

            return parameters;
        }

        private static string EscapeString(string str)
        {
            return str.Replace("\"", "\\\"");
        }
    }
}
