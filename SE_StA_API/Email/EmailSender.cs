using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace SE_StA_API.Email {
    public class EmailSender : IEmailSender {
        private readonly EmailConfiguration _emailConfiguration;
        public EmailSender(EmailConfiguration emailConfiguration) {
            _emailConfiguration = emailConfiguration;
        }

        public void SendEmail(Message message) {
            var email = CreateEmailMessage(message);
            Send(email);
        }
        public async Task SendEmailAsync(Message message) {
            var email = CreateEmailMessage(message);
            await SendAsync(email);
        }

        private MimeMessage CreateEmailMessage(Message message) {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(
                _emailConfiguration.FromDisplayName,
                _emailConfiguration.From));
            email.To.AddRange(message.To);
            email.Subject = message.Subject;
            email.Bcc.Add(InternetAddress.Parse("se.sta@chabbay.de"));

            var bodyBuilder = new BodyBuilder {
                HtmlBody = string.Format(
                    "<p>{0}</p>",
                    message.Content)
            };

            if ((message.Attachments != null) && message.Attachments.Any()) {
                byte[] fileBytes;
                foreach (var attachment in message.Attachments) {
                    using (var stream = new MemoryStream()) {
                        attachment.CopyTo(stream);
                        fileBytes = stream.ToArray();
                    }
                    bodyBuilder.Attachments.Add(
                        attachment.FileName,
                        fileBytes,
                        ContentType.Parse(attachment.ContentType));
                }
            }

            email.Body = bodyBuilder.ToMessageBody();
            return email;
        }

        private void Send(MimeMessage message) {
            using (var client = new SmtpClient()) {
                try {
                    client.Connect(
                        _emailConfiguration.SmtpServer,
                        _emailConfiguration.Port,
                        SecureSocketOptions.StartTls);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(
                        _emailConfiguration.Username,
                        _emailConfiguration.Password);
                    client.Send(message);
                } catch {
                    throw;
                } finally {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
        private async Task SendAsync(MimeMessage message) {
            using (var client = new SmtpClient()) {
                try {
                    await client.ConnectAsync(
                        _emailConfiguration.SmtpServer,
                        _emailConfiguration.Port,
                        SecureSocketOptions.StartTls);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(
                        _emailConfiguration.Username,
                        _emailConfiguration.Password);
                    await client.SendAsync(message);
                } catch {
                    throw;
                } finally {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }
    }
}