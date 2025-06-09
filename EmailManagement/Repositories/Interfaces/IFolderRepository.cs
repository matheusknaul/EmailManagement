using EmailManagement.Models;

namespace EmailManagement.Repositories.Interfaces
{
    public interface IFolderRepository
    {
        Task<List<Folder>> GetAllFoldersAsync(int userId);
        Task<Folder> GetFolderByIdAsync(int id);
        Task<bool> CreateFolderAsync(Folder folder);
        Task<bool> DeleteFolderAsync(int id);
    }

}
