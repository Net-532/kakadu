using Kakadu.Backend.Entities;
using System.Collections.Generic;

namespace Kakadu.Backend.Repositories
{
    public interface IOrderRepository
    {
        Order GetById(int id);
        List<Order> GetAll();
        void Save(Order order);
        void ChangeStatus(int id, string status);
    }
}


