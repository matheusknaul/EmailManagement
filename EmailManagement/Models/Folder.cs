using System.ComponentModel.DataAnnotations;

namespace EmailManagement.Models
{
    public class Folder
    {

        int Id { get; set; }
        [Required(ErrorMessage = "O campo 'Nome' é obrigatório.")]
        public string Name { get; set; }

        public List<string> TargetSubjects { get; set; }

        public List<Recipient> TargetRecipients { get; set; }

    }
}
