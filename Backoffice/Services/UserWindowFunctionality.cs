using System.Collections.Generic;
using Kakadu.Backend.Entities;
using Kakadu.Backend.Repositories;
using Kakadu.Backend.Services;
using Kakadu.Backoffice.Services;


namespace Kakadu.Backoffice.Views
{
    public class UserManager: IManageable<User>
    {
        private UserService UserServ;

        public UserManager()
        {
            UserServ = new UserService(new UserRepositoryDB());
        }

        public List<User> LoadItems()
        {
            return UserServ.GetAll();
        }

        public void AddItem(User item)
        {
            UserServ.Save(item);
        }

        public void DeleteItem(int Id)
        {
            UserServ.DeleteById(Id);
        }

        public void EditItem(int itemId, User newItem)
        {
            UserServ.Update(itemId, newItem);
        }

    }
}
