namespace NotificationServices.Models
{
    public class SNS
    {
        public APNS APNS { get; set; }
        public APNS APNS_SANDBOX { get; set; }
        public GCM GCM { get; set; }
    }
}
