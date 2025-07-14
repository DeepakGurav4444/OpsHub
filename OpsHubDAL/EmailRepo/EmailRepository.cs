using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Net.Http.Headers;
using System.Linq;
using System.Text.RegularExpressions;
using OpsHubCommonUtility.Response;
using OpsHubCommonUtility.Logger;


namespace OpsHubDAL.EmailRepo
{
    public class EmailRepository : IEmailRepository
    {
        private IConfiguration _configuration;
        private readonly ILoggerManager _loggerManager;

        public EmailRepository(IConfiguration configuration, ILoggerManager loggerManager)
        {
            _configuration = configuration;
            _loggerManager = loggerManager;
        }
        public void SendEmail(string emailbody, string Subject)
        {
            ResultWithDataDTO<int> resultWithDataBO = new ResultWithDataDTO<int>
            {
                IsSuccessful = false
            };
            try
            {
                string environment = _configuration["OpsHubData:Environment"];
                if (environment != "DEV")
                {
                    MailMessage mail = new MailMessage();
                    mail.To.Add("exceptions@ifelsesolutions.com");
                    mail.From = new MailAddress("dev@ifelsesolutions.com", "Exception Alert");
                    string url = @$"{environment}";
                    mail.Subject = $"{Subject} from OpsHub Project | {url}";
                    mail.Body = emailbody;
                    mail.IsBodyHtml = true;
                    System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                    smtp.Port = 587;
                    smtp.Host = "smtp.gmail.com";
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("dev@ifelsesolutions.com", "Abc$7744");
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

