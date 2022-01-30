using CoreIdentity.Models;

using Microsoft.Extensions.Options;

using System.Net;
using System.Net.Mail;

namespace CoreIdentity.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpSendEmailModel smtpSendEmailModel;

        public EmailService(IOptions<SmtpSendEmailModel> smtpSendEmailModel)
        {
            this.smtpSendEmailModel = smtpSendEmailModel.Value;
        }
        public async Task SendEmailAsync(string from, string to, string subject, string body)
        {
            var message = new MailMessage(from, to, subject, body);

            using var emailClient = new SmtpClient(smtpSendEmailModel.Host, smtpSendEmailModel.Port);
            emailClient.Credentials = new NetworkCredential(smtpSendEmailModel.UserName, smtpSendEmailModel.Password);
            await emailClient.SendMailAsync(message);
        }
    }
}
