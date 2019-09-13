using System;
namespace NotificationServices
{
    public class Configuration
    {
        private static readonly Lazy<Configuration>
                lazy =
                new Lazy<Configuration>
                    (() => new Configuration());

        public static Configuration Instance { get { return lazy.Value; } }

        private Configuration()
        {
        }

        public string AWSAccessKey { get; set; }

        public string AWSIOSPNSARN { get; set; }

        public string AWSFCMPNSARN { get; set; }

        public string AWSSecretKey { get; set; }

        public string AWSRegionEndPoint { get; set; }

        public string FirebaseServerKey { get; set; }
    }
}
