using EmailManagement.Models;

namespace EmailManagement.Repositories.Interfaces
{
    public interface IFolderRepository
    {
        Task<List<Folder>> GetAllFolders(int userId);
        Task<Folder> GetFolderById(int id);
        Task<bool> CreateFolder(Folder folder);
        Task<bool> DeleteFolder(int id);
    }

}
