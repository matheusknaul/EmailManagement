using EmailManagement.Models;
using EmailManagement.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using MySql.Data.MySqlClient;
using System.Configuration;


namespace EmailManagement.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            var users = new List<User>();
            var query = "SELECT Id, Name, Email, PerfilPicturePath, CreatedAt FROM email_management.User";

            using var connection = new MySqlConnection(_connectionString);
            using var command = new MySqlCommand(query, connection);
            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                users.Add(new User
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Email = reader.GetString(2),
                    PerfilPicturePath = reader.GetString(3),
                    CreatedAt = reader.GetDateTime(4)
                });
            }
            return users;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            User user = null;
            var query = "SELECT Id, Name, Email, PerfilPicturePath, CreatedAt FROM email_management.User WHERE Id = @Id";

            using var connection = new MySqlConnection(_connectionString);
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                user = new User
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Email = reader.GetString(2),
                    PerfilPicturePath = reader.GetString(3),
                    CreatedAt = reader.GetDateTime(4)
                };
            }
            return user;

        }

        public async Task<bool> SaveUserAsync(User user)
        {
            var query = "INSERT INTO email_management.User (Name, Password, Email, PerfilPicturePath) VALUES (@Name, @Password, @Email, @PerfilPicturePath);";
            using var connection = new MySqlConnection(_connectionString);
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Name", user.Name);
            command.Parameters.AddWithValue("@Password", user.Password);
            command.Parameters.AddWithValue("@Email", user.Email);

            var perfilPicturePath = user.PerfilPicturePath ?? string.Empty;

            command.Parameters.AddWithValue("@PerfilPicturePath", perfilPicturePath);

            await connection.OpenAsync();
            var result = await command.ExecuteNonQueryAsync();

            return result > 0;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var query = "DELETE FROM email_management.User WHERE Id = @Id";
            using var connection = new MySqlConnection(_connectionString);
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);
            await connection.OpenAsync();
            var result = await command.ExecuteNonQueryAsync();
            return result > 0;
        }
    }
}
