namespace TripFlip.Services.Configurations
{
    public class MailServiceConfiguration
    {
        public string SendGridApiKey { get; set; }

        public string RegisteredUserNotificationFilename { get; set; }

        public string UserStatisticMessageFilename { get; set; }

        public string AppFromEmail { get; set; }

        public string AppFromName { get; set; }
    }
}
