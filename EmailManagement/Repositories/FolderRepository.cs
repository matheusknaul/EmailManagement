using EmailManagement.Models;
using EmailManagement.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EmailManagement.Repositories
{
    public class FolderRepository : IFolderRepository
    {
        private readonly string _connectionString;

        public FolderRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<Folder>> GetAllFoldersByUserAsync(int userId)
        {
            var folders = new List<Folder>();
            var query = "SELECT Id, Name, UserId, isSystem FROM email_management.Folder WHERE UserId = @UserId";

            using var connection = new MySqlConnection(_connectionString);
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserId", userId);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                folders.Add(new Folder
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    UserId = reader.GetInt32(2),
                    isSystem = reader.GetBoolean(3)
                });
            }

            return folders;
        }

        public async Task<Folder> GetFolderByIdAsync(int id)
        {
            Folder folder = null;

            var query = "SELECT Id, Name, UserId, isSystem from email_management.Folder WHERE Id = @Id";

            using var connection = new MySqlConnection(_connectionString);
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                folder = new Folder
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    UserId = reader.GetInt32(2),
                    isSystem = reader.GetBoolean(3),
                    TargetSubjects = new List<string>(),
                    TargetRecipients = new List<Recipient>()
                };
            }
            return folder;
        }

        public async Task<bool> CreateFolderAsync(Folder folder)
        {
            var query = "INSERT INTO email_management.Folder (Name, UserId, isSystem) VALUES (@Name, @UserId, @isSystem);";

            using var connection = new MySqlConnection(_connectionString);
            using var command = new MySqlCommand(query, connection);
            {
                command.Parameters.AddWithValue("@Name", folder.Name);
                command.Parameters.AddWithValue("@UserId", folder.UserId);
                command.Parameters.AddWithValue("@isSystem", folder.isSystem);

                await connection.OpenAsync();
                var result = await command.ExecuteNonQueryAsync();
                return result > 0;
            }
        }

        public async Task<bool> DeleteFolderAsync(int id)
        {
            var queryCheck = "SELECT isSystem FROM email_management.Folder WHERE id = @Id";
            var queryDelete = "DELETE FROM email_management.Folder WHERE id = @Id";

            using var connection = new MySqlConnection(_connectionString);
            using var commandCheck = new MySqlCommand(queryCheck, connection);
            {
                commandCheck.Parameters.AddWithValue("@Id", id);
                await connection.OpenAsync();

                var isSystem = (bool?)await commandCheck.ExecuteScalarAsync();

                if (isSystem == true)
                {
                    return false;
                }

                using (var commandDelete = new MySqlCommand(queryDelete, connection))
                {
                    commandDelete.Parameters.AddWithValue("@Id", id);
                    var result = await commandDelete.ExecuteNonQueryAsync();
                    return result > 0;
                }

            }
        }

    }
}
