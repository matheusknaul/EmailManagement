using EmailManagement.Models;

namespace EmailManagement.Repositories.Interfaces
{
    public interface IFolderRepository
    {
        Task<List<Folder>> GetAllFoldersByUserAsync(int userId);
        Task<Folder> GetFolderByIdAsync(int id);
        Task<bool> SaveFolderAsync(Folder folder);
        Task<bool> DeleteFolderAsync(int id);
    }

}
