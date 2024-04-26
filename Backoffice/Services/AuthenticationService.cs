using System.Linq;
using System.Security.Authentication;
using System.Xml.Linq;
using Kakadu.Backend.Entities;

namespace Kakadu.Backoffice.Services
{
    public class AuthenticationService
    {
        public User Authenticate(string username, string password)
        {
            XDocument xmlDoc = XDocument.Load("..\\..\\..\\..\\Backend\\Backend\\data\\users.xml");
            var userNode = xmlDoc.Descendants("user")
                .FirstOrDefault(x => x.Element("Username").Value == username && x.Element("Password").Value == password);


            if (userNode == null)
            {
                throw new AuthenticationException("Користувача не знайдено або пароль неправильний!");
            }

            var user = new User(
                int.Parse(userNode.Element("Id").Value),
                userNode.Element("Username").Value,
                userNode.Element("FirstName").Value,
                userNode.Element("LastName").Value,
                userNode.Element("Password").Value
            );

            return user;
        }
    }
}