using backend;
using System;
using System.Collections.Generic;
using System.Xml;

namespace Backend
{
    public class ProductRepositoryXML : IProductRepository
    {
        private readonly string filePath = "D:/3CoursePart2/Dot/kakadu/Backend/Backend/data/";

        public ProductRepositoryXML(string filePath)
        {
            this.filePath = filePath;
        }

        public void deleteById(int id)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            XmlNode node = doc.SelectSingleNode($"/products/product[Id = '{id}']");
            if (node != null)
            {
                node.ParentNode.RemoveChild(node);
                doc.Save(filePath);
            }
        }

        public List<Product> getAll()
        {
            List<Product> products = new List<Product>();

            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            foreach (XmlNode node in doc.SelectNodes("/products/product"))
            {
                Product product = new Product();
                product.Id = int.Parse(node.SelectSingleNode("Id").InnerText);
                product.Title = node.SelectSingleNode("Title").InnerText;
                product.Price = decimal.Parse(node.SelectSingleNode("Price").InnerText);
                product.PhotoUrl = node.SelectSingleNode("PhotoUrl").InnerText;
                product.Description = node.SelectSingleNode("Description").InnerText;
                products.Add(product);
            }

            return products;
        }

        public Product getById(int id)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            XmlNode node = doc.SelectSingleNode($"/products/product[Id = '{id}']");
            if (node != null)
            {
                Product product = new Product();
                product.Id = int.Parse(node.SelectSingleNode("Id").InnerText);
                product.Title = node.SelectSingleNode("Title").InnerText;
                product.Price = decimal.Parse(node.SelectSingleNode("Price").InnerText);
                product.PhotoUrl = node.SelectSingleNode("PhotoUrl").InnerText;
                product.Description = node.SelectSingleNode("Description").InnerText;
                return product;
            }
            return null;
        }

        public void save(Product product)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            XmlNode root = doc.DocumentElement;

            XmlNode productElement = doc.CreateElement("product");

            XmlNode idElement = doc.CreateElement("Id");
            idElement.InnerText = product.Id.ToString();
            productElement.AppendChild(idElement);

            XmlNode titleElement = doc.CreateElement("Title");
            titleElement.InnerText = product.Title;
            productElement.AppendChild(titleElement);

            XmlNode priceElement = doc.CreateElement("Price");
            priceElement.InnerText = product.Price.ToString();
            productElement.AppendChild(priceElement);

            XmlNode photoUrlElement = doc.CreateElement("PhotoUrl");
            photoUrlElement.InnerText = product.PhotoUrl;
            productElement.AppendChild(photoUrlElement);

            XmlNode descriptionElement = doc.CreateElement("Description");
            descriptionElement.InnerText = product.Description;
            productElement.AppendChild(descriptionElement);

            root.AppendChild(productElement);

            doc.Save(filePath);
        }

        public void update(int id, Product product)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            XmlNode node = doc.SelectSingleNode($"/products/product[Id = '{id}']");
            if (node != null)
            {
                node.SelectSingleNode("Id").InnerText = product.Id.ToString();
                node.SelectSingleNode("Title").InnerText = product.Title;
                node.SelectSingleNode("Price").InnerText = product.Price.ToString();
                node.SelectSingleNode("PhotoUrl").InnerText = product.PhotoUrl;
                node.SelectSingleNode("Description").InnerText = product.Description;
                doc.Save(filePath);
            }
        }
    }
}
