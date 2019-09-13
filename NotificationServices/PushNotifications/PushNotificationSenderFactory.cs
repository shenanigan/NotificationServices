namespace NotificationServices.PushNotifications
{
    public static class PushNotificationSenderFactory
    {
        public static IPushNotificationSender GetAWSPushNotificationSender()
        {
            return new AWSPushNotificationSender();
        }

        public static IPushNotificationSender GetFirebasePushNotificationSender()
        {
            return new FirebasePushNotificationSender();
        }
    }
}
