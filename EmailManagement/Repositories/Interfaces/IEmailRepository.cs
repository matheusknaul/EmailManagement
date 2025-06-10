using EmailManagement.Models;

namespace EmailManagement.Repositories.Interfaces
{
    public interface IEmailRepository
    {
        Task<List<Email>> GetAllEmailsByUserAsync(int userId);
        Task<Email> GetEmailByIdAsync(int id);
        Task<bool> SaveEmailAsync(Email email);
        Task<bool> DeleteEmailAsync(int id);
    }
}
