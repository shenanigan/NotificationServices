using System;
using System.Threading.Tasks;
using Amazon.Runtime;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using NotificationServices.Models;
using Newtonsoft.Json;

namespace NotificationServices.PushNotifications
{
    class AWSPushNotificationSender : IPushNotificationSender
    {
        private readonly AmazonSimpleNotificationServiceClient _snsClient;

        public AWSPushNotificationSender()
        {
            var awsCredentials = new BasicAWSCredentials(Configuration.Instance.AWSAccessKey, Configuration.Instance.AWSSecretKey);
            _snsClient = new AmazonSimpleNotificationServiceClient(awsCredentials, Amazon.RegionEndpoint.GetBySystemName(Configuration.Instance.AWSRegionEndPoint));
        }

        public async Task<PushNotificationResponseDto> CreateAWSEndpointAsync(string Topic, string Token, string Platform)
        {
            CreatePlatformEndpointRequest cpeReq = new CreatePlatformEndpointRequest();
            if (Platform.Equals("ios", StringComparison.CurrentCultureIgnoreCase))
            {
                cpeReq.PlatformApplicationArn = Configuration.Instance.AWSIOSPNSARN;
            }
            else if (Platform.Equals("android", StringComparison.CurrentCultureIgnoreCase))
            {
                cpeReq.PlatformApplicationArn = Configuration.Instance.AWSFCMPNSARN;
            }
            else
            {
                throw new Exception("Only ios & android are supporeted as platforms for now");
            }

            cpeReq.Token = Token;
            CreatePlatformEndpointResponse cpeRes = await _snsClient.CreatePlatformEndpointAsync(cpeReq);
            if (cpeRes.HttpStatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(cpeRes.ToString());
            }


            CreateTopicRequest request = new CreateTopicRequest(Topic);
            var topicResponse = await _snsClient.CreateTopicAsync(request);
            if (topicResponse.HttpStatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(topicResponse.ToString());
            }

            var subscribeRequest = new SubscribeRequest(topicResponse.TopicArn, "application", cpeRes.EndpointArn);
            var subscribeResponse = await _snsClient.SubscribeAsync(subscribeRequest);
            if (subscribeResponse.HttpStatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(subscribeResponse.ToString());
            }

            return new PushNotificationResponseDto
            {
                TopicArn = topicResponse.TopicArn,
                SubscriptionArn = subscribeResponse.SubscriptionArn,
                EndPointArn = cpeRes.EndpointArn
            };
        }

        public async Task UnsubscribeAsync(string SubscriptionArn)
        {

            UnsubscribeRequest request = new UnsubscribeRequest
            {
                SubscriptionArn = SubscriptionArn
            };

            await _snsClient.UnsubscribeAsync(request);
        }

        public async Task SendAWSPushNotificationAsync(string TopicArn, SNS Payload)
        {
            PublishRequest publishReq = new PublishRequest()
            {
                TopicArn = TopicArn,
                MessageStructure = "json",
                Message = JsonConvert.SerializeObject(Payload)
            };
            PublishResponse response = await _snsClient.PublishAsync(publishReq);

            if (response.HttpStatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(response.ToString());
            }
        }

        public void SendFirebasePushNotification(string[] deviceTokens, string title, string body, object data)
        {
            throw new NotImplementedException();
        }
    }
}