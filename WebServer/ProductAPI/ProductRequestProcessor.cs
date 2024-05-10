using Kakadu.Backend.Entities;
using Kakadu.Backend.Services;
using Kakadu.WebServer;

namespace WebServer.ProductAPI
{
    public class ProductRequestProcessor
    {
        private readonly IProductService productService;
        private readonly ProductToJsonConverter jsonConverter;

        // Adjusted accessibility for ProductToJsonConverter
        public ProductRequestProcessor(IProductService productService, ProductToJsonConverter productToJsonConverter)
        {
            this.productService = productService;
            this.jsonConverter = productToJsonConverter;
        }

        public HttpResponse Process(HttpRequest httpRequest)
        {
            List<Product> products = productService.GetAll(); ;

            string responseBody = jsonConverter.Convert(products);

            HttpResponse response = new HttpResponse();

            response.Body = responseBody;

            return response;
        }
    }
}
