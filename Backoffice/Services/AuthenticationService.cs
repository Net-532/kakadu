using Kakadu.Backend.Entities;
using Kakadu.Backend.Services;

namespace Kakadu.Backoffice.Services
{
    public class AuthenticationService
    {
        private IUserService _userService;

        public AuthenticationService(IUserService userService)
        {
            _userService = userService;
        }

        public User Authenticate(string username, string password)
        {
           var user =  _userService.GetByUsernameAndPassword(username, password);

            if (user == null)
            {
                throw new AuthenticationException("Користувача не знайдено або пароль неправильний!");
            }

            return user;
        }
    }
}