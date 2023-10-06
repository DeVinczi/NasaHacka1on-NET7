using MailKit.Net.Smtp;
using MimeKit;
using NasaHacka1on.Mail;

namespace NasaHacka1on.Services
{
    public interface IMailService
    {
        Task<bool> SendMailAsync(MailData mailData);
    }
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        public MailService(IConfiguration configuration)
        {
            _mailSettings = new MailSettings();
            configuration.Bind("MailSettings", _mailSettings);
        }

        public async Task<bool> SendMailAsync(MailData mailData)
        {
            try
            {
                using (var emailMessage = new MimeMessage())
                {
                    var emailFrom = new MailboxAddress(_mailSettings.SenderName, _mailSettings.SenderEmail);
                    emailMessage.From.Add(emailFrom);

                    var emailTo = new MailboxAddress(mailData.EmailToName, mailData.EmailToId);
                    emailMessage.To.Add(emailTo);

                    emailMessage.Subject = mailData.EmailSubject;

                    var bodyBuilder = new BodyBuilder();
                    bodyBuilder.TextBody = mailData.EmailBody;

                    emailMessage.Body = bodyBuilder.ToMessageBody();

                    using (var mailClient = new SmtpClient())
                    {
                        mailClient.ServerCertificateValidationCallback = (sender, certificate, certChainType, errors) => true;
                        await mailClient.ConnectAsync(_mailSettings.Server, _mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
                        await mailClient.AuthenticateAsync(_mailSettings.UserName, _mailSettings.Password);
                        await mailClient.SendAsync(emailMessage);
                        await mailClient.DisconnectAsync(true);
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
