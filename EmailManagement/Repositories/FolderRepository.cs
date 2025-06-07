using EmailManagement.Models;
using EmailManagement.Repositories.Interfaces;

namespace EmailManagement.Repositories
{
    public class FolderRepository : IFolderRepository
    {
        public Task<bool> CreateFolderAsync(Folder folder)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteFolderAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Folder>> GetAllFoldersAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<Folder> GetFolderByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
