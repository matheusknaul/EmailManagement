using EmailManagement.Models;
using EmailManagement.Repositories.Interfaces;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using Org.BouncyCastle.Asn1.X509;
using System.Data;

namespace EmailManagement.Repositories
{
    public class EmailRepository : IEmailRepository
    {
        private readonly string _connectionString;

        public EmailRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<Email>> GetAllEmailsByUserAsync(int userId)
        {
            var emails = new List<Email>();
            var query = "SELECT Id, Subject, Body, user_id, recipient_id, date_sent FROM email_management.Email WHERE user_id = @UserId";

            using var connection = new MySqlConnection(_connectionString);
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserId", userId);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                emails.Add(new Email
                {
                    Id = reader.GetInt32(0),
                    Subject = reader.GetString(1),
                    Body = reader.GetString(2),
                    UserId = reader.GetInt32(3),
                    RecipientId = reader.GetInt32(4),
                    DateSent = reader.GetDateTime(5)
                });
            }

            return emails;

        }

        public async Task<List<Email>> GetEmailsBySubjectAsync(string subject)
        {
            List<Email> emails = new List<Email>();

            var query = "SELECT id, subject FROM email_management.Email WHERE subject COLLATE utf8mb4_general_ci LIKE @Subject";

            using var connection = new MySqlConnection(_connectionString);
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Subject", $"%{subject}%");

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                emails.Add(new Email
                {
                    Id = reader.GetInt32(0),
                    Subject = reader.GetString(1),
                });
            }
            return emails;
        }

        public async Task<Email> GetEmailByIdAsync(int id)
        {
            Email email = null;

            var query = "SELECT Id, Subject, Body, user_id, recipient_id, date_sent FROM email_management.Email WHERE Id = @Id";

            using var connection = new MySqlConnection(_connectionString);
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                email = new Email
                {
                    Id = reader.GetInt32(0),
                    Subject = reader.GetString(1),
                    Body = reader.GetString(2),
                    UserId = reader.GetInt32(3),
                    RecipientId = reader.GetInt32(4),
                    DateSent = reader.GetDateTime(5)
                };
            }
            return email;
        }

        public async Task<bool> SaveEmailAsync(Email email)
        {
            var query = "INSERT INTO email_management.Email (Subject, Body, user_id, recipient_id, date_sent) VALUES (@Subject, @Body, @UserId, @RecipientId, @DateSent);";

            using var connection = new MySqlConnection(_connectionString);
            using var command = new MySqlCommand(query, connection);
            {
                command.Parameters.AddWithValue("@Subject", email.Subject);
                command.Parameters.AddWithValue("@Body", email.Body);
                command.Parameters.AddWithValue("@UserId", email.UserId);
                command.Parameters.AddWithValue("@RecipientId", email.RecipientId);
                command.Parameters.AddWithValue("@DateSent", email.DateSent);

                await connection.OpenAsync();
                var result = await command.ExecuteNonQueryAsync();
                return result > 0;
            }
        }

        public async Task<bool> DeleteEmailAsync(int id)
        {
            var query = "DELETE FROM email_management.Email WHERE Id = @Id";
            using var connection = new MySqlConnection(_connectionString);
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);
            await connection.OpenAsync();
            var result = await command.ExecuteNonQueryAsync();
            return result > 0;
        }
    }
}
