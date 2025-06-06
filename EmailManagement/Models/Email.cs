using System.ComponentModel.DataAnnotations;

namespace EmailManagement.Models
{
    public class Email
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo 'Assunto' é obrigatório.")]
        public string Assunto { get; set; }
        [Required(ErrorMessage = "O corpo do email é obrigatório.")]
        public string Corpo { get; set; }
        [EmailAddress(ErrorMessage = "Email inválido.")]
        public string Destinatario { get; set; }
        public DateTime DataEnvio { get; set; }



    }
}
