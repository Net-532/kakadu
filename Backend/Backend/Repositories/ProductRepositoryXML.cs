using Kakadu.Backend.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace Kakadu.Backend.Repositories
{
    public class ProductRepositoryXML : IProductRepository
    {
        private static readonly string filePath = "./data/products.xml";

        private Product ConvertToProduct(XmlNode node)
        {
            Product product = new Product();
            product.Id = int.Parse(node.SelectSingleNode("Id").InnerText);
            product.Title = node.SelectSingleNode("Title").InnerText;
            product.Price = decimal.Parse(node.SelectSingleNode("Price").InnerText, CultureInfo.InvariantCulture);
            product.PhotoUrl = node.SelectSingleNode("PhotoUrl").InnerText;
            product.Description = node.SelectSingleNode("Description").InnerText;
            return product;
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
                Product product = ConvertToProduct(node);
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
                return ConvertToProduct(node);
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
            priceElement.InnerText = product.Price.ToString(CultureInfo.InvariantCulture);
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
                node.SelectSingleNode("Price").InnerText = product.Price.ToString(CultureInfo.InvariantCulture);
                node.SelectSingleNode("PhotoUrl").InnerText = product.PhotoUrl;
                node.SelectSingleNode("Description").InnerText = product.Description;
                doc.Save(filePath);
            }
        }
    }
}
