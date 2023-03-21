using Microsoft.AspNetCore.Identity;
using MimeKit;

namespace SE_StA_API.Email {
    public class Message {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public IFormFileCollection Attachments { get; set; }
        public Message(IEnumerable<IdentityUser> to, string subject, string content, IFormFileCollection attachments) {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x => new MailboxAddress(x.UserName, x.Email)));
            Subject = subject;
            Content = content;
            Attachments = attachments;
        }
    }
}