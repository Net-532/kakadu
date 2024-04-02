﻿using Kakadu.Backend.Entities;
using System.Collections.Generic;

namespace Kakadu.Backend.Services
{
     public interface IOrderService
    {
        List<Order> GetAll();

        Order GetById(int id);

        void Save(Order order);

        void DeleteById(int id);

        void Update(int id, Order order);
    }
}
