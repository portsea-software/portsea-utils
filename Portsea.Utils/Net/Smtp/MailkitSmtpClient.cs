using System.Threading.Tasks;

using MailKit.Security;
using MimeKit;

namespace Portsea.Utils.Net.Smtp
{
    public class MailkitSmtpClient
    {
        private const int DefaultSmptPort = 25;
        private const SecureSocketOptions DefaultSslOptions = SecureSocketOptions.Auto;

        private readonly string smtpHost;
        private readonly int smtpPort;
        private readonly SecureSocketOptions sslOptions;
        private readonly string username;
        private readonly string password;

        public MailkitSmtpClient(string smtpHost, string password)
            : this(smtpHost, string.Empty, password, DefaultSmptPort, DefaultSslOptions)
        {
        }

        public MailkitSmtpClient(string smtpHost, string username, string password)
            : this(smtpHost, username, password, DefaultSmptPort, DefaultSslOptions)
        {
        }

        public MailkitSmtpClient(string smtpHost, string username, string password, int smtpPort, SecureSocketOptions sslOptions)
        {
            this.smtpHost = smtpHost;
            this.username = username;
            this.password = password;
            this.smtpPort = smtpPort;
            this.sslOptions = sslOptions;
        }

        public async Task<MimeMessage> SendEmail(MimeMessage message)
        {
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                await client.ConnectAsync(this.smtpHost, this.smtpPort, this.sslOptions);

                string login = string.IsNullOrWhiteSpace(this.username) ? message.GetFromEmailAddress() : this.username;
                await client.AuthenticateAsync(login, this.password);

                await client.SendAsync(message);

                await client.DisconnectAsync(true);
            }

            return message;
        }
    }
}