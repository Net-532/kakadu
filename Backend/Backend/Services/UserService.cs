using backend;
using Backend.Repositories;
using System.Collections.Generic;

namespace Backend.Services
{
    internal class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public void deleteById(int id)
        {
            var user = userRepository.getById(id);
            if (user == null)
            {
                throw new UserNotFoundException($"Користувача з вказаним id {id} не знайдено");
            }
            userRepository.deleteById(id);
        }

        public List<User> getAll()
        {
            return userRepository.getAll();
        }

        public User getById(int id)
        {
            var user = userRepository.getById(id);
            if (user == null)
            {
                throw new UserNotFoundException($"Користувача з вказаним id {id} не знайдено");
            }
            return user;
        }

        public void save(User user)
        {
            userRepository.save(user);
        }

        public void update(int id, User user)
        {
            var existingUser = userRepository.getById(id);
            if (existingUser == null)
            {
                throw new UserNotFoundException($"Користувача з вказаним id {id} не знайдено");
            }
            userRepository.update(id, existingUser);
        }
    }
}
