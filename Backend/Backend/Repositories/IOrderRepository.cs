using Kakadu.Backend.Entities;
using System;
using System.Collections.Generic;

namespace Kakadu.Backend.Repositories
{
    public interface IOrderRepository
    {
        List<Order> GetAll();

        Order GetById(int id);

        Order GetByNumber(int number);

        Order Save(Order order);

        void ChangeStatus(int id, string status);

        List<Order> GetAllByUpdatedAt(DateTime from, DateTime to);
    }
}
