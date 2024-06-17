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
            try
            {
                return _userService.GetByUsernameAndPassword(username, password);
            }
            catch (EntityNotFoundException)
            {
                throw new AuthenticationException("Користувача не знайдено або пароль неправильний!");
            }
        }
    }
}