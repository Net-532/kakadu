using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows;
using Kakadu.Backend.Entities;
using Kakadu.Backend.Repositories;
using Kakadu.Backend.Services;
using Kakadu.Backoffice.Services;

namespace Kakadu.Backoffice.Views
{
    public class OrderManager : IManageable<Order>
    {
        private OrderService OrderServ;
        private OrderPrintService OrderPrintServ;

        public OrderManager()
        {
            OrderServ = new OrderService(new OrderRepositoryXML());
            OrderPrintServ = new OrderPrintService(OrderServ, new OrderToPlainTextConverter(new ProductService(new ProductRepositoryXML())), new PrintService());
        }

        public void Print(int OrderId)
        {
           OrderPrintServ.Print(OrderId);

        }

        public List<Order> LoadItems()
        {
            return OrderServ.GetAll();
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
