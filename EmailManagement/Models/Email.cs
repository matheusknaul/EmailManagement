namespace EmailManagement.Models
{
    public class Email
    {
        public int Id { get; set; }
        public string Assunto { get; set; }
        public string Corpo { get; set; }
        public string Destinatario { get; set; }
        public DateTime DataEnvio { get; set; }



    }
}
