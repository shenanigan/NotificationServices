using System;
namespace NotificationServices.Models
{
    public class APNS
    {
        public APS Aps { get; set; }
    }

    public class APS
    {
        public string Alert { get; set; }
    }
}
