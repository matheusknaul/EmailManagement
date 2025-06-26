using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmailManagement.Services;
using MySqlX.XDevAPI.Common;

namespace EmailManagement.Tests.Services
{
    public class EmailServiceTest
    {
        [Fact]
        public void GetEmailsBySearchTest()
        {
            var query = "teste relatório azul";
            var esperado = new string[] { "teste", "relatório", "azul" };
            var emailService = new EmailService();

            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            var result = emailService.GetEmailsBySearch(query);
            stopwatch.Stop();

            System.Console.WriteLine($"Tempo de execução: {stopwatch.ElapsedMilliseconds} ms");

            Assert.Equal(esperado, result);

        }
    }
    
}
