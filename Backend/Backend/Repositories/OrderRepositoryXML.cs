
using Kakadu.Backend.Entities;
using System;
using System.Collections.Generic;
using System.Globalization; 
using System.Xml; 

namespace Kakadu.Backend.Repositories
{
    public class OrderRepositoryXML : IOrderRepository
    {
        private static readonly string filePath = "./data/orders.xml";

        public Order GetById(int id)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            XmlNode node = doc.SelectSingleNode($"/orders/order[Id = '{id}']");
            if (node != null)
            {
                return ConvertToOrder(node);
            }
            return null;
        }

        public List<Order> GetAll()
        {
            List<Order> orders = new List<Order>();

            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            foreach (XmlNode node in doc.SelectNodes("/orders/order"))
            {
                Order order = ConvertToOrder(node);
                orders.Add(order);
            }

            return orders;
        }

        public void Save(Order order)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            XmlNode root = doc.DocumentElement;

            int nextOrderNumber = getNextOrderNumber();

            XmlNode orderElement = doc.CreateElement("order");

            XmlNode orderNumberElement = doc.CreateElement("OrderNumber");
            orderNumberElement.InnerText = nextOrderNumber.ToString(); 
            orderElement.AppendChild(orderNumberElement);

            XmlNode idElement = doc.CreateElement("Id");
            idElement.InnerText = order.Id.ToString();
            orderElement.AppendChild(idElement);

            XmlNode totalPriceElement = doc.CreateElement("TotalPrice");
            totalPriceElement.InnerText = order.TotalPrice.ToString(CultureInfo.InvariantCulture);
            orderElement.AppendChild(totalPriceElement);

            XmlNode orderDateElement = doc.CreateElement("OrderDate");
            orderDateElement.InnerText = order.OrderDate.ToString();
            orderElement.AppendChild(orderDateElement);

            XmlNode statusElement = doc.CreateElement("Status");
            statusElement.InnerText = order.Status;
            orderElement.AppendChild(statusElement);

            root.AppendChild(orderElement);

            doc.Save(filePath);
        }


        public void ChangeStatus(int id, string status)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            XmlNode node = doc.SelectSingleNode($"/orders/order[Id = '{id}']");
            if (node != null)
            {
                node.SelectSingleNode("Status").InnerText = status;
                doc.Save(filePath);
            }
        }

        private Order ConvertToOrder(XmlNode node)
        {
            Order order = new Order();
            order.OrderNumber = int.Parse(node.SelectSingleNode("OrderNumber").InnerText);
            order.Id = int.Parse(node.SelectSingleNode("Id").InnerText);
            order.TotalPrice = decimal.Parse(node.SelectSingleNode("TotalPrice").InnerText, CultureInfo.InvariantCulture);
            order.OrderDate = DateTime.Parse(node.SelectSingleNode("OrderDate").InnerText);
            order.Status = node.SelectSingleNode("Status").InnerText;

          
            XmlNodeList itemNodes = node.SelectNodes("Items/Item");
           
                foreach (XmlNode itemNode in itemNodes)
                {
                    OrderItem item = new OrderItem
                    {
                        Id = int.Parse(itemNode.SelectSingleNode("Id").InnerText),
                        ProductId = int.Parse(itemNode.SelectSingleNode("ProductId").InnerText),
                        Quantity = int.Parse(itemNode.SelectSingleNode("Quantity").InnerText),
                        Price = decimal.Parse(itemNode.SelectSingleNode("Price").InnerText, CultureInfo.InvariantCulture),
                        OrderId = order.Id
                    };
                    order.Items.Add(item);
                }
            

            return order;
        }

        public int getNextOrderNumber()
        {
            List<Order> Orders = GetAll();
            int MaxNumber= 0;
            foreach (Order o in Orders)
            {
                if (o.OrderNumber > MaxNumber)
                    MaxNumber = o.OrderNumber;
            }

     
            return MaxNumber + 1;
        }

    }
}

