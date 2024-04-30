using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kakadu.WebServer.Order
{
    public class OrderRequestProcessor
    {

        private readonly OrderService _orderService;

        public OrderRequestProcessor(OrderService orderService)
        {
            _orderService = orderService;
        }

        public void Process(OrderRequest orderRequest)
        {
            var order = new Order
            {
                Items = orderRequest.Items.Select(item => new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                }).ToList()
            };

            _orderService.save(order);
        }
    }

}

