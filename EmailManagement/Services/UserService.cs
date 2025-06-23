using EmailManagement.Models;

namespace EmailManagement.Services
{
    public class UserService
    {

        public void CreateDefaultFolders(int userId)
        {
            var folders = new List<Folder>
            {
                new Folder { UserId = userId, Name = "Inbox", isSystem = true },
                new Folder { UserId = userId, Name = "Sent", isSystem = true },
                new Folder { UserId = userId, Name = "Drafts", isSystem = true },
                new Folder { UserId = userId, Name = "Spam", isSystem = true },
                new Folder { UserId = userId, Name = "Trash", isSystem = true }
            };

        }

    }
}
