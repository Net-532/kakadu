
namespace Kakadu.WebServer.Order
{
    public class OrderRequest
    {

        public List<OrderItemRequest> Items { get; set; } = new List<OrderItemRequest>();

        public class OrderItemRequest
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; }

            public OrderItemRequest()
            {
                
            }

            public OrderItemRequest(int productId, int quantity)
            {
                ProductId = productId;
                Quantity = quantity;
            }
        }

    }
}
