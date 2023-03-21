namespace SE_StA_API.Email {
    public interface IEmailSender {
        void SendEmail(Message message);
        Task SendEmailAsync(Message message);
    }
}