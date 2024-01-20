using Identity.Contracts;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Identity.Services
{
    public class EmailService : IEmailService
    {
        SMTPSettings m_smtpSettings;
        public EmailService(IOptions<SMTPSettings> smtpSettings)
        {
            m_smtpSettings = smtpSettings.Value;
        }

        public async Task SendAsync(string stToEmail, string stSubject, string stBody)
        {
            var stMessage = new MailMessage(m_smtpSettings.Email,
                stToEmail,
                stSubject,
                stBody);

            using (var emailClient = new SmtpClient(m_smtpSettings.Host))
            {
                emailClient.Port = m_smtpSettings.Port;
                emailClient.EnableSsl = true;
                emailClient.Credentials = new NetworkCredential(m_smtpSettings.Email, m_smtpSettings.Password);
                await emailClient.SendMailAsync(stMessage);
            }
        }
    }
}
