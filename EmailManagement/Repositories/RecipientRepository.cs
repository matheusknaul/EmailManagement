using EmailManagement.Models;
using EmailManagement.Repositories.Interfaces;
using MySql.Data.MySqlClient;

namespace EmailManagement.Repositories
{
    public class RecipientRepository : IRecipientRepository
    {
        private readonly string _connectionString;
        public RecipientRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<Recipient>> GetAllRecipientsAsync()
        {
            var recipients = new List<Recipient>();
            var query =  "SELECT Id, Name, Email FROM email_management.Recipient";

            using var connection = new MySqlConnection(_connectionString);
            using var command = new MySqlCommand(query, connection);
            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                recipients.Add(new Recipient
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Email = reader.GetString(2)
                });
            }
            return recipients;
        }

        public async Task<Recipient> GetRecipientByIdAsync(int id)
        {
            Recipient recipient = null;

            var query = "SELECT Id, Name, Email FROM email_management.Recipient WHERE Id = @Id";
            using var connection = new MySqlConnection(_connectionString);
            using var command = new MySqlCommand(query, connection);
            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                recipient = new Recipient
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Email = reader.GetString(2)
                };
            }
            return recipient;
        }

        public async Task<bool> SaveRecipientAsync(Recipient recipient)
        {
            var query = "INSERT INTO email_management.Recipient (Name, Email) VALUES (@Name, @Email)";
            using var connection = new MySqlConnection(_connectionString);
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Name", recipient.Name);
            command.Parameters.AddWithValue("@Email", recipient.Email);
            await connection.OpenAsync();
            var result = await command.ExecuteNonQueryAsync();

            return result > 0;
        }
        public async Task<bool> DeleteRecipientAsync(int id)
        {
            var query = "DELETE FROM email_management.Recipient WHERE Id = @Id";
            using var connection = new MySqlConnection(_connectionString);
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);
            await connection.OpenAsync();
            var result = await command.ExecuteNonQueryAsync();
            return result > 0;
        }
    }
}
