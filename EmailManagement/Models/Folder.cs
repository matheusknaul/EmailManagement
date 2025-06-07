using System.ComponentModel.DataAnnotations;

namespace EmailManagement.Models
{
    public class Folder
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "O campo 'Nome' é obrigatório.")]
        public string Name { get; set; }
        public List<string> TargetSubjects { get; set; }
        public List<Recipient> TargetRecipients { get; set; }
        public int UserId { get; set; }
        public bool isSystem { get; set; } = false;

    }
}
