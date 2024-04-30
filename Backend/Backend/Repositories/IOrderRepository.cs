using Kakadu.Backend.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kakadu.Backend.Repositories
{
    public interface IOrderRepository
    {
        List<Order> GetAll();

        Order GetById(int id);

        void Save(Order order);
    }
}
