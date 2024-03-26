using Kakadu.Backend.Entities;
using System.Collections.Generic;


namespace Kakadu.Backend.Repositories
{
    public interface IUserRepository
    {
        List<User> GetAll();
        User GetById(int id);

        void Save(User user);

        void DeleteById(int id);

        void Update(int id, User user);
    }
}
