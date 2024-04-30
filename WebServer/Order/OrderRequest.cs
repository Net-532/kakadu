using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kakadu.WebServer.Order
{
    public class OrderRequest
    {
        List<OrderItemRequest> items { get; set; }

        public class OrderItemRequest
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; }
        }

    }
}
