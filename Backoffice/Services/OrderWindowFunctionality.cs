using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows;
using Kakadu.Backend.Entities;
using Kakadu.Backend.Repositories;
using Kakadu.Backend.Services;
using Kakadu.Backoffice.Services;
using Kakadu.Backoffice.Models;
using Backoffice.Views;

namespace Kakadu.Backoffice.Views
{
    public class OrderManager : IManageable<Order>
    {
        private OrderService OrderServ;
        private OrderPrintService OrderPrintServ;
        private ProductService ProductService;

        public OrderManager()
        {
            var orderItemRepository = new OrderItemRepositoryDB();
            OrderServ = new OrderService(new OrderRepositoryDB(orderItemRepository));
            OrderPrintServ = new OrderPrintService(OrderServ, new OrderToPlainTextConverter(new ProductService(new ProductRepositoryDB())), new PrintService());
            ProductService= new ProductService(new ProductRepositoryDB());
        }

        public void Print(int OrderId)
        {
           OrderPrintServ.Print(OrderId);

        }

        public List<Order> LoadItems()
        {
            List<Order> orders = OrderServ.GetAll();

            foreach(Order order in orders)
            {
                List<OrderItem> items = new List<OrderItem>();
                foreach(OrderItem orderItem in order.Items)
                {
                    OrderItemModel model = new OrderItemModel();
                    model.Id = orderItem.Id;
                    model.ProductId = orderItem.ProductId;
                    model.Price = orderItem.Price;
                    model.Quantity = orderItem.Quantity;
                    model.Amount= orderItem.Amount;
                    model.OrderId= orderItem.OrderId;
                    model.ProductTitle = ProductService.GetById(model.ProductId).Title;
                    items.Add(model);
                }
                order.Items = items;
            }

            return orders;
        }

        public void AddItem(Order item)
        {
            //there's no add functionality 
        }

        public void ChangeStatus(int id,string status)
        {
            OrderServ.ChangeStatus(id,status);
        }

        public Order GetById(int id)
        {
            return OrderServ.GetById(id);
        }

        public Order GetByNumber(int number)
        {
            return OrderServ.GetByNumber(number);
        }

        public void DeleteItem(int Id)
        {
            //there's no delete functionality
        }

        public void EditItem(int itemId, Order newItem)
        {
            //there's no edit functionality
        }


    }
}
