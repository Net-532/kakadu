
namespace Kakadu.WebServer.Order
{
    public class OrderRequest
    {

        public List<OrderItemRequest> Items { get; set; } = new List<OrderItemRequest>();

        public class OrderItemRequest
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; }
            
        }

    }
}
