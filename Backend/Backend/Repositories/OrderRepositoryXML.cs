using Kakadu.Backend.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using System.Linq;

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

        public Order GetByNumber(int number) {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            XmlNode node = doc.SelectSingleNode($"/orders/order[OrderNumber = '{number}']");
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

        private int getNextOrderId()
        {
            List<Order> Orders = GetAll();

            if (Orders != null && Orders.Count > 0)
            {
                int MaxNumber = Orders.Max(o => o.Id);
                return MaxNumber + 1;
            }

            return 1;
        }

        public Order Save(Order order)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            XmlNode root = doc.DocumentElement;

            XmlNode orderElement = doc.CreateElement("order");

            XmlNode orderNumberElement = doc.CreateElement("OrderNumber");

            order.OrderNumber = getNextOrderNumber();
            orderNumberElement.InnerText = order.OrderNumber.ToString();
            orderElement.AppendChild(orderNumberElement);

            order.Id = getNextOrderId();

            XmlNode idElement = doc.CreateElement("Id");
            idElement.InnerText = order.Id.ToString();
            orderElement.AppendChild(idElement);

            XmlNode totalPriceElement = doc.CreateElement("TotalPrice");
            totalPriceElement.InnerText = order.TotalPrice.ToString(CultureInfo.InvariantCulture);
            orderElement.AppendChild(totalPriceElement);

            XmlNode orderDateElement = doc.CreateElement("OrderDate");
            orderDateElement.InnerText = order.OrderDate.ToString("s", CultureInfo.InvariantCulture);
            orderElement.AppendChild(orderDateElement);

            XmlNode updateAtElement = doc.CreateElement("UpdatedAt");
            updateAtElement.InnerText = order.UpdatedAt.ToString("s", CultureInfo.InvariantCulture);
            orderElement.AppendChild(updateAtElement);


            XmlNode statusElement = doc.CreateElement("Status");
            statusElement.InnerText = order.Status;
            orderElement.AppendChild(statusElement);


            XmlNode itemsElement = doc.CreateElement("items");
            foreach (OrderItem item in order.Items)
            {
                XmlNode itemElement = doc.CreateElement("item");

                XmlNode itemIdElement = doc.CreateElement("Id");
                itemIdElement.InnerText = item.Id.ToString();
                itemElement.AppendChild(itemIdElement);

                XmlNode orderIdElement = doc.CreateElement("OrderId");
                orderIdElement.InnerText = order.Id.ToString();
                itemElement.AppendChild(orderIdElement);

                XmlNode productIdElement = doc.CreateElement("ProductId");
                productIdElement.InnerText = item.ProductId.ToString();
                itemElement.AppendChild(productIdElement);

                XmlNode quantityElement = doc.CreateElement("Quantity");
                quantityElement.InnerText = item.Quantity.ToString();
                itemElement.AppendChild(quantityElement);

                XmlNode priceElement = doc.CreateElement("Price");
                priceElement.InnerText = item.Price.ToString(CultureInfo.InvariantCulture);
                itemElement.AppendChild(priceElement);

                XmlNode amountElement = doc.CreateElement("Amount");
                amountElement.InnerText = item.Amount.ToString(CultureInfo.InvariantCulture);
                itemElement.AppendChild(amountElement);

                itemsElement.AppendChild(itemElement);
            }

            orderElement.AppendChild(itemsElement);
            root.AppendChild(orderElement);

            doc.Save(filePath);
            return order;

        }

        public void ChangeStatus(int id, string status)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            XmlNode node = doc.SelectSingleNode($"/orders/order[Id = '{id}']");
            if (node != null)
            {
                node.SelectSingleNode("Status").InnerText = status;
                node.SelectSingleNode("UpdateAT").InnerText = DateTime.Now.ToString("s", CultureInfo.InvariantCulture); ;
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


            XmlNodeList itemNodes = node.SelectNodes("items/item");

            foreach (XmlNode itemNode in itemNodes)
            {
                OrderItem item = new OrderItem
                {
                    Id = int.Parse(itemNode.SelectSingleNode("Id").InnerText),
                    ProductId = int.Parse(itemNode.SelectSingleNode("ProductId").InnerText),
                    Quantity = int.Parse(itemNode.SelectSingleNode("Quantity").InnerText),
                    Price = decimal.Parse(itemNode.SelectSingleNode("Price").InnerText, CultureInfo.InvariantCulture),
                    Amount = decimal.Parse(itemNode.SelectSingleNode("Amount").InnerText, CultureInfo.InvariantCulture),
                    OrderId = order.Id
                };
                order.Items.Add(item);
            }

            return order;
        }

        public int getNextOrderNumber()
        {
            List<Order> Orders = GetAll();
            int MaxNumber = 0;
            foreach (Order o in Orders)
            {
                if (o.OrderNumber > MaxNumber)
                    MaxNumber = o.OrderNumber;
            }


            return MaxNumber + 1;
        }

    }
}

