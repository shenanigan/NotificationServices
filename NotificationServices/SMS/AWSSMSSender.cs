using System.Threading.Tasks;
using Amazon.Runtime;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;

namespace NotificationServices.SMS
{
    class AWSSMSSender : ISMSSender
    {
        private readonly AmazonSimpleNotificationServiceClient _snsClient;

        internal AWSSMSSender()
        {
            var awsCredentials = new BasicAWSCredentials(Configuration.Instance.AWSAccessKey, Configuration.Instance.AWSSecretKey);
            _snsClient = new AmazonSimpleNotificationServiceClient(awsCredentials, Amazon.RegionEndpoint.GetBySystemName( Configuration.Instance.AWSRegionEndPoint));
        }

        public async Task SendSmsAsync(string number, string message, string SenderId = "Notif", string SMSType = "Transactional")
        {
            if (Configuration.Instance.AWSAccessKey == null ||
                Configuration.Instance.AWSSecretKey == null ||
                Configuration.Instance.AWSRegionEndPoint == null)
            {
                throw new System.Exception("Initiate the configuration values for AWS");
            }

            PublishRequest pubRequest = new PublishRequest();
            pubRequest.MessageAttributes["AWS.SNS.SMS.SenderID"] =
                new MessageAttributeValue { StringValue = SenderId, DataType = "String" };
            pubRequest.MessageAttributes["AWS.SNS.SMS.SMSType"] =
                new MessageAttributeValue { StringValue = SMSType, DataType = "String" };

            pubRequest.Message = message;
            pubRequest.PhoneNumber = number;

            var response = await _snsClient.PublishAsync(pubRequest);

            if (response.HttpStatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new System.Exception(response.ToString());
            }
        }
    }
}
