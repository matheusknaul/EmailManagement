using System.ComponentModel.DataAnnotations;

namespace EmailManagement.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo 'Nome' é obrigatório.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "O campo 'Password' é obrigatório.")]
        public string Password { get; set; }
        [EmailAddress(ErrorMessage = "Email inválido.")]
        public string Email { get; set; }
        public string AvatarURL { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<Folder> Folders { get; set; }

    }
}
