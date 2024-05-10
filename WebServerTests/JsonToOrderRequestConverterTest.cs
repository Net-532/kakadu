using Kakadu.WebServer.Order;

namespace WebServerTests
{
    public class JsonToOrderRequestConverterTest
    {
        [Fact]
        public void ConvertToJson()
        {
            string json = """
                { 
                "items": [ 
                     {
                     "productId": 10,
                      "quantity": 2
                    },
                     {
                     "productId": 1,
                      "quantity": 5
                    }
                  ]
                }
                """;

            var converter = new JsonToOrderRequestConverter();
            var actualOrderRequest = converter.Convert(json);
            // check items size
            var expectedItemsSize = 2;
            Assert.Equal(expectedItemsSize, actualOrderRequest.Items.Count);
    
            // check items values
            var actualOrterItem1 = actualOrderRequest.Items[0];
            var expectedOrderItem1 = new OrderRequest.OrderItemRequest(10, 2);
            Assert.Equal(expectedOrderItem1.ProductId, actualOrterItem1.ProductId);
            Assert.Equal(expectedOrderItem1.Quantity, actualOrterItem1.Quantity);

            var actualOrterItem2 = actualOrderRequest.Items[1];
            var expectedOrderItem2 = new OrderRequest.OrderItemRequest(1, 5);
            Assert.Equal(expectedOrderItem2.ProductId, actualOrterItem2.ProductId);
            Assert.Equal(expectedOrderItem2.Quantity, actualOrterItem2.Quantity);
        }
    }
}