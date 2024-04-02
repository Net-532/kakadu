using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repositories
{
    internal interface IOrderRepository
    {
        Order GetById(int id);
        List<Order> GetAll();
        void Save(Order order);
        void ChangeStatus(int id, string status);
    }
}


