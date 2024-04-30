using Kakadu.Backend.Entities;
using Kakadu.Backend.Repositories;
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

        public void Save(Order order)
        {
            orderRepository.Save(order);
        }
    }
}
