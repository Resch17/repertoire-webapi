using repertoire_webapi.Models;

namespace repertoire_webapi.Interfaces
{
    public interface IUserRepository
    {
        void AddUser(User user);
        User GetUser(int userId);
    }
}