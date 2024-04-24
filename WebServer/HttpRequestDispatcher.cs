using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServer
{
    public enum HttpStatus
    {
        OK = 200,
        BadRequest = 400,
        NotFound = 404,
        InternalServerError = 500
    }

    public class HttpResponse
    {
        public HttpStatus Status { get; set; }
        public string Body { get; set; }
        public Dictionary<string, string> Headers { get; set; }

        public HttpResponse()
        {
            Headers = new Dictionary<string, string>();
        }
    }

    public class HttpRequest
    {
        public string RootPath { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public string Body { get; set; }
        public Dictionary<string, string> Parameters { get; set; }
    }

    public class HttpRequestDispatcher
    {
        public HttpResponse Dispatch(HttpRequest httpRequest)
        {
            HttpResponse response = new HttpResponse();

            // Визначте логіку обробки запитів в залежності від rootPath
            switch (httpRequest.RootPath)
            {
                case "/home":
                    // Виклик сервісу для сторінки домашньої
                    response = ProcessHomeRequest(httpRequest);
                    break;
                default:
                    // Якщо rootPath не відповідає жодному відомому шляху
                    response.Status = HttpStatus.NotFound;
                    response.Body = "Сторінку не знайдено";
                    break;
            }

            // Додайте необхідні заголовки до відповіді
            response.Headers.Add("Content-Type", "text/plain");

            return response;
        }

        private HttpResponse ProcessHomeRequest(HttpRequest request)
        {
            // Реалізуйте обробку запиту для сторінки домашньої тут
            HttpResponse response = new HttpResponse();
            response.Status = HttpStatus.OK;
            response.Body = "Це домашня сторінка";
            return response;
        }


    }
}
