using backend;
using System;
using System.Collections.Generic;

namespace Backend
{
    internal class UserService : IUserService
    {
        private readonly IUserService userService;
        UserService(IUserService userService)
        {
            this.userService = userService;
        }
        List<User> IUserService.getAll()
        {
            List<User> users = null;

            users = userService.getAll();

            if (users == null)
            {
                throw new UserNotFoundException("User NOT found!");
            }

            return users;
        }
        User IUserService.getById(int id)
        {
            User user = null;
            user = userService.getById(id);
            if (user == null)
            {
                throw new UserNotFoundException("User NOT found!");
            }
            return user;
        }
        void IUserService.save(User user)
        {
            if (user != null)
            {
                userService.save(user);
            }
            else
            {
                throw new UserNotFoundException("User has NOT been saved!");
            }
        }
        void IUserService.deleteById(int id)
        {
            userService.deleteById(id);
        }
        void IUserService.update(int id, User user)
        {
            userService.update(id, user);
        }
    }
}
