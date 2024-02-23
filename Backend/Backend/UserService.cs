using backend;
using System;
using System.Collections.Generic;

namespace Backend
{
    internal class UserService : IUserService
    {
        List<User> IUserService.getAll()
        {
            List<User> users = null;

            users = getAllUsersXML();

            if (users == null)
            {
                throw new UserNotFoundException("User NOT found!");
            }

            return users;
        }
        User IUserService.getById(int id)
        {
            User user = null;
            user = getUserByIDXML(2);
            if (user == null)
            {
                throw new UserNotFoundException("User NOT found!");
            }
            return user;
        }
        void IUserService.save(User user)
        {
            saveUserXML(user);
        }
        void IUserService.deleteById(int id)
        {
            deleteUserByID(id);
        }
        void IUserService.update(int id, User user)
        {
            updateUser(id, user);
        }
    }
}
