using backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repositories
{
    interface IUserRepository
    {
        List<User> getAll();
        User getById(int id);

        void save(User user);
        void deleteById(int id);

        void update(int id, User user);
    }
}
