using Kakadu.Backend.Entities;
using Kakadu.Backend.Repositories;
using System.Collections.Generic;

namespace Kakadu.Backend.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        
        public void DeleteById(int id)
        {
            var user = userRepository.GetById(id);
            if (user == null)
            {
                throw new EntityNotFoundException($"Користувача з вказаним id {id} не знайдено");
            }
            userRepository.DeleteById(id);
        }

        public List<User> GetAll()
        {
            return userRepository.GetAll();
        }

        public User GetById(int id)
        {
            var user = userRepository.GetById(id);
            if (user == null)
            {
                throw new EntityNotFoundException($"Користувача з вказаним id {id} не знайдено");
            }
            return user;
        }

        public void Save(User user)
        {
            userRepository.Save(user);
        }

        public void Update(int id, User user)
        {
            var existingUser = userRepository.GetById(id);
            if (existingUser == null)
            {
                throw new EntityNotFoundException($"Користувача з вказаним id {id} не знайдено");
            }
            userRepository.Update(id, user);
        }

        public User GetByUsernameAndPassword(string username, string password)
        {
            var user = userRepository.GetByUsernameAndPassword(username, password);
            if (user == null)
            {
                throw new EntityNotFoundException($"Користувача з вказаним ім'ям користувача '{username}' та паролем не знайдено");
            }
            return user;
        }

    }
}
