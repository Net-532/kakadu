﻿using Kakadu.Backend.Entities;
using System.Collections.Generic;

namespace Kakadu.Backend.Repositories
{
    public interface IOrderItemRepository
    {
        List<OrderItem> GetAll();
        OrderItem GetById(int id);
        void Save(OrderItem orderItem);
        void Update(OrderItem orderItem);
        void Delete(int id);
    }
}
