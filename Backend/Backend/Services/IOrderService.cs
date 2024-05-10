using Kakadu.Backend.Entities;
using System.Collections.Generic;

namespace Kakadu.Backend.Services
{
     public interface IOrderService
    {
        List<Order> GetAll();

        Order GetById(int id);

        Order Save(Order order);

        void ChangeStatus(int id, string status);

    }
}
