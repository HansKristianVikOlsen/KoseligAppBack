using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SendGrid.Helpers.Mail;
using Microsoft.AspNetCore.Cors;

namespace KoseligMelding
{
    
    public static class Function1
    {
        
        [FunctionName("Function1")]
        public static void  Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            [SendGrid(ApiKey = "SendGridApiKey")] ICollector<SendGridMessage> send,

            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request. Melding sendt");

            string requestBody =  new StreamReader(req.Body).ReadToEnd();

            var email = requestBody.ToString(); //Regex.Match(licenseFileContents, @"^Email\:\·(.+)$", RegexOptions.Multiline).Groups[1].Value;
           
            log.LogInformation($"Got order from {email}\n Order Id:");
            SendGridMessage message = new SendGridMessage();
            // message.From = new EmailAddress("hans.kristian.olsen@bouvet.no"); //new EmailAddress(Environment.GetEnvironmentVariable("EmailSender"));
            message.From = new EmailAddress(email);
            message.AddTo(email);
            
            
            
            message.Subject = "Koselig melding";
            message.HtmlContent = "Du er snill og grei";
            send.Add(message);
          //  return new OkObjectResult($"Koselig melding sendt!");



        }
    }
}
