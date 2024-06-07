using Kakadu.Backend.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backoffice.Models
{
    class OrderItemModel : OrderItem
    {
        public string ProductTitle { get; set; }
    }
}
