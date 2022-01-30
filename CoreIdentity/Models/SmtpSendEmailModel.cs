namespace CoreIdentity.Models
{
    public class SmtpSendEmailModel
    {
        public SmtpSendEmailModel()
        {
            Host = string.Empty;
            UserName = string.Empty;
            Password = string.Empty;
        }
        public string Host { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
