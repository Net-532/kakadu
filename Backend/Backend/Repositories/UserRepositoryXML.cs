using Kakadu.Backend.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Kakadu.Backend.Repositories
{
    public class UserRepositoryXML : IUserRepository
    {
        private static readonly string filePath = "./data/users.xml";

        private User ConvertToUser(XmlNode node)
        {
            User User = new User();
            User.Id = int.Parse(node.SelectSingleNode("Id").InnerText);
            User.Username = node.SelectSingleNode("Username").InnerText;
            User.FirstName = node.SelectSingleNode("FirstName").InnerText;
            User.LastName = node.SelectSingleNode("LastName").InnerText;
            User.Password = node.SelectSingleNode("Password").InnerText;
            return User;
        }

        public void DeleteById(int id)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            XmlNode node = doc.SelectSingleNode($"/users/user[Id = '{id}']");
            if (node != null)
            {
                node.ParentNode.RemoveChild(node);
                doc.Save(filePath);
            }
        }

        public List<User> GetAll()
        {
            List<User> Users = new List<User>();

            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            foreach (XmlNode node in doc.SelectNodes("/users/user"))
            {
                User User = ConvertToUser(node);
                Users.Add(User);
            }

            return Users;
        }

        public User GetById(int id)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            XmlNode node = doc.SelectSingleNode($"/users/user[Id = '{id}']");
            if (node != null)
            {
                return ConvertToUser(node);
            }
            return null;
        }

        public void Save(User User)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            XmlNode root = doc.DocumentElement;

            XmlNode UserElement = doc.CreateElement("user");

            User.Id = getNextUserId();

            XmlNode idElement = doc.CreateElement("Id");
            idElement.InnerText = User.Id.ToString();
            UserElement.AppendChild(idElement);

            XmlNode UsernameElement = doc.CreateElement("Username");
            UsernameElement.InnerText = User.Username;
            UserElement.AppendChild(UsernameElement);

            XmlNode FirstNameElement = doc.CreateElement("FirstName");
            FirstNameElement.InnerText = User.FirstName;
            UserElement.AppendChild(FirstNameElement);

            XmlNode LastNameElement = doc.CreateElement("LastName");
            LastNameElement.InnerText = User.LastName;
            UserElement.AppendChild(LastNameElement);

            XmlNode PasswordElement = doc.CreateElement("Password");
            PasswordElement.InnerText = User.Password;
            UserElement.AppendChild(PasswordElement);

            root.AppendChild(UserElement);

            doc.Save(filePath);
        }

        public void Update(int id, User User)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            XmlNode node = doc.SelectSingleNode($"/users/user[Id = '{id}']");
            if (node != null)
            {
                node.SelectSingleNode("Id").InnerText = User.Id.ToString();
                node.SelectSingleNode("Username").InnerText = User.Username;
                node.SelectSingleNode("FirstName").InnerText = User.FirstName;
                node.SelectSingleNode("LastName").InnerText = User.LastName;
                node.SelectSingleNode("Password").InnerText = User.Password;
                doc.Save(filePath);
            }
        }
        private int getNextUserId()
        {
            List<User> Users = GetAll();

            if (Users != null && Users.Count > 0)
            {
                int MaxNumber = Users.Max(o => o.Id);
                return MaxNumber + 1;
            }

            return 1;
        }
        public User GetByUsernameAndPassword(string username, string password)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            XmlNode node = xmlDoc.SelectSingleNode($"/users/user[Username = '{username}' and Password = '{password}']");
            if (node != null)
            {
                return ConvertToUser(node);
            }
            return null;
        }
       
    }
}
