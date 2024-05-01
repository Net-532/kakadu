using Kakadu.Backend.Repositories;
using Kakadu.Backend.Entities;


namespace Kakadu.WebServer.Order
{
    public class OrderRequestProcessor
    {
        private readonly IOrderRepository _orderService;

        public OrderRequestProcessor(IOrderRepository orderService)
        {
            _orderService = orderService;
        }

        public void Process(OrderRequest orderRequest)
        {
            var order = new Backend.Entities.Order
            {
                Items = orderRequest.Items.Select(item => new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    
                }).ToList(),
                TotalPrice = 0, 
                OrderDate = DateTime.UtcNow
            };

            _orderService.Save(order);
        }
    }
}