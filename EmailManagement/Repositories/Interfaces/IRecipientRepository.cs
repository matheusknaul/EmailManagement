using EmailManagement.Models;

namespace EmailManagement.Repositories.Interfaces
{
    public interface IRecipientRepository
    {
        Task<List<Recipient>> GetAllRecipientsAsync();
        Task<Recipient> GetRecipientByIdAsync(int id);
        Task<bool> SaveRecipientAsync(Recipient recipient);
        Task<bool> DeleteRecipientAsync(int id);
    }
}
