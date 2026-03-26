using CinemaCore.Models;

namespace CinemaCore.Interfaces
{
    public interface IAuthService
    {
        string Register(User user);
        string Login(string email, string password);
    }
}
