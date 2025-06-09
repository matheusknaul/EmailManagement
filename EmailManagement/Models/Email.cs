using System.ComponentModel.DataAnnotations;

namespace EmailManagement.Models
{
    public class Email
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo 'Assunto' é obrigatório.")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "O corpo do email é obrigatório.")]
        public string Body { get; set; }
        [EmailAddress(ErrorMessage = "Email inválido.")]
        public int UserId { get; set; }
        public int RecipientId { get; set; }
        public DateTime DateSent { get; set; }



    }
}
