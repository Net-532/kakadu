using Kakadu.Backend.Entities;
using Kakadu.Backend.Repositories;
using System;
using System.Collections.Generic;

namespace Kakadu.Backend.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public List<Order> GetAll()
        {
            return orderRepository.GetAll();
        }

        public Order GetById(int id)
        {
            var order = orderRepository.GetById(id);
            if (order == null)
            {
                throw new OrderNotFoundException($"Замовлення з вказаним id {id} не знайдено");
            }
            return order;
        }

        public Order GetByNumber(int number)
        {
            var order = orderRepository.GetByNumber(number);
            return order;
        }

        public Order Save(Order order)
        {
            return orderRepository.Save(order); 
        }

        public void ChangeStatus(int id, string status)
        {
            var order = orderRepository.GetById(id);
            if (order == null)
            {
                throw new OrderNotFoundException($"Замовлення з вказаним id {id} не знайдено");
            }

            orderRepository.ChangeStatus(id, status);
        }

        public List<Order> GetAllByUpdatedAt(DateTime from, DateTime to)
        {
            return orderRepository.GetAllByUpdatedAt(from, to);
        }
    }
}
