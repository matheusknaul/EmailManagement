using EmailManagement.Models;

namespace EmailManagement.Services
{
    public class EmailService
    {

        //Testando primeiro com Strings
        public string[] GetEmailsBySearch(string query)
        {
            string[] emails = query.Split(' ');

            //Aqui testarei e brincarei com n maneiras de fazer essa busca.
            //TODO: Métrica de elapsed time.
            //Maneira 1:
            for(int i = 0; i < emails.Length; i++)
            {
                
            }

            return emails;

        }

        public List<Email> GetEmailByFilter() {

            return new List<Email>();
        }
    }
}
