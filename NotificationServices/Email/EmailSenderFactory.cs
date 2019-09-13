namespace NotificationServices.Email
{
    public static class EmailSenderFactory
    {
        public static IEmailSender GetAWSEmailSender()
        {
            return new AWSEmailSender();
        }
    }
}
