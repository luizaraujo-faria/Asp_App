using Asp_App.DTOs;
using Asp_App.Models;

namespace Asp_App.Repositories.Contracts
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User GetUserById(int id);
        void SignUp(CreateUserDTO newUser);
        void SignIn(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
    }
}