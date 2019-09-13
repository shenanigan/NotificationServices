namespace NotificationServices.SMS
{
    public static class SMSSenderFactory
    {
        public static ISMSSender GetAWSSMSSender()
        {
            return new AWSSMSSender();
        }
    }
}
