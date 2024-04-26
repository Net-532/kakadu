using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Kakadu.WebServer
{
    public enum HttpStatus
    {
        OK = 200,
        BadRequest = 400,
        NotFound = 404,
        InternalServerError = 500
    }
    public class HttpRequestDispatcher
    {
        public HttpResponse Dispatch(HttpRequest httpRequest)
        {
            HttpResponse response = new HttpResponse();

            switch (httpRequest.RootPath)
            {
                case "/products":
                    
                    response = ProcessProductsRequest(httpRequest);
                    break;
                default:
                    
                    response.Status = HttpStatus.NotFound;
                    response.Body = $"No endpoint found for the {httpRequest.RootPath}";

;
                    break;
            }

            response.Headers.Add("Content-Type", "application/json");
            response.Headers.Add("Access-Control-Allow-Origin", "*");

            return response;
        }

        private HttpResponse ProcessProductsRequest(HttpRequest request)
        {
            
            HttpResponse response = new HttpResponse();
            response.Status = HttpStatus.OK;
            response.Body = "Це домашня сторінка";
            return response;
        }}}
