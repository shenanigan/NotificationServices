using System;
namespace NotificationServices.Models
{
    public class GCM
    {
        public Data Data { get; set; }
    }

    public class Data
    {
        public string Message { get; set; }
    }
}
