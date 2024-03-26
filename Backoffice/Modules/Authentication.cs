using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Kakadu.Backend.Entities;
using System.Windows;

public class AuthenticationException : ApplicationException
{
    public AuthenticationException(string message) : base(message)
    {
        //MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
    }
}

public class AuthenticationService
{
    public static User Authenticate(string username, string password)
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