using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repositories
{
    internal interface IOrderRepository
    {

        Order getById(int id);
        List<Order> getAll();
        void save(Order order);
        void changeStatus(int id, string status);
    }
}
