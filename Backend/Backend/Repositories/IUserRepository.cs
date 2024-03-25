using Kakadu.Backend.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
