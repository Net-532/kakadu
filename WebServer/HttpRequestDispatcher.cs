using Kakadu.Backend.Repositories;
using Kakadu.Backend.Services;
using Kakadu.WebServer.OrderAPI;
using Kakadu.WebServer.ProductAPI;

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
        private static IProductRepository productRepository = new ProductRepositoryXML();
        private static IProductService productService = new ProductService(productRepository);
        private static IOrderRepository orderRepository = new OrderRepositoryXML();
        private static ProductRequestProcessor productRequestProcessor = new ProductRequestProcessor(productService, new ProductToJsonConverter());
        private static PrintOrderRequestProcessor printOrderRequestProcessor = new PrintOrderRequestProcessor();

        public HttpResponse Dispatch(HttpRequest httpRequest)
        {
            HttpResponse response = new HttpResponse();

            switch (httpRequest.RootPath)
            {
                case "/products":
                    response = ProcessProductsRequest(httpRequest);
                    break;
                case "/orders":
                    response = ProcessOrdersRequest(httpRequest);
                    break;
                case "/print":
                    response = printOrderRequestProcessor.Process(httpRequest);
                    break;
                default:
                    response.Status = HttpStatus.NotFound;
                    response.Body = $"No endpoint found for the {httpRequest.RootPath}";
                    break;
            }
            response.Status = HttpStatus.OK;
            response.Headers.Add("Content-Type", "application/json");
            response.Headers.Add("Access-Control-Allow-Origin", "*");

            return response;
        }

        private HttpResponse ProcessProductsRequest(HttpRequest request)
        {
            return productRequestProcessor.Process(request);
        }

        private HttpResponse ProcessOrdersRequest(HttpRequest request)
        {

            var order = new Backend.Entities.Order();
            orderRepository.Save(order);

            var response = new HttpResponse();
            response.Body = "{}";
            return response;
        }

    }
}
