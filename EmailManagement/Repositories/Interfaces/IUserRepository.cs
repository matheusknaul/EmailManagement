using EmailManagement.Models;

namespace EmailManagement.Repositories.Interfaces
{
    public interface IUserRepository
    {

        Task<List<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<bool> SaveUserAsync(User user);
        Task<bool> DeleteUserAsync(int id);
    }
}
