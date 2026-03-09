using Asp_App.Models;

namespace Asp_App.Repositories.Contracts
{
    public interface IUserContract
    {
        IEnumerable<User> GetAll();
        User GetUserById(int id);
        void SignUp(User newUser);
        void SignIn(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
    }
}