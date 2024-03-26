using System;
using System.Collections.Generic;


namespace Kakadu.Backend.Entities
{
    public class Order
    {
        public int OrderNumber { get; set; }
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public List<OrderItem> Items { get; set; }
    }
  
}
