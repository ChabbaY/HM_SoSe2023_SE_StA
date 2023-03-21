namespace SE_StA_API.Email {
    public class EmailConfiguration {
        public string From { get; set; }
        public string FromDisplayName { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}