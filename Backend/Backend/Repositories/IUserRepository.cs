using Kakadu.Backend.Entities;
using System;
using System.Collections.Generic;

namespace Kakadu.Backend.Repositories
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
