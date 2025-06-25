using EmailManagement.Models;

namespace EmailManagement.Services
{
    public class EmailService
    {

        //Testando primeiro com Strings
        public string[] GetEmailsBySearch(string query)
        {

            //Adicionar lógica de busca de emails aqui

            string[] emails = query.Split(' ');


            return emails;

        }

        public List<Email> GetEmailByFilter() {

            return new List<Email>();
        }
    }
}
