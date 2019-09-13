using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NotificationServices.Models;
using NotificationServices.Models.Firebase;

namespace NotificationServices.PushNotifications
{
    class FirebasePushNotificationSender : IPushNotificationSender
    {
        public Task<PushNotificationResponseDto> CreateAWSEndpointAsync(string Topic, string Token, string Platform)
        {
            throw new System.NotImplementedException();
        }

        public Task SendAWSPushNotificationAsync(string TopicArn, SNS Payload)
        {
            throw new System.NotImplementedException();
        }

        public void SendFirebasePushNotification(string[] deviceTokens, string title, string body, object data)
        {

            if (Configuration.Instance.FirebaseServerKey == null)
            {
                throw new System.Exception("Initiate the configuration values for Firebase via Configuration.Instance");
            }

            var messageInformation = new Message()
            {
                notification = new Notification()
                {
                    title = title,
                    text = body
                },
                data = data,
                registration_ids = deviceTokens
            };
            //Object to JSON STRUCTURE => using Newtonsoft.Json;
            string jsonMessage = JsonConvert.SerializeObject(messageInformation);

            // Create request to Firebase API
            var request = new HttpRequestMessage(HttpMethod.Post, "https://fcm.googleapis.com/fcm/send");
            request.Headers.TryAddWithoutValidation("Authorization", "key =" + Configuration.Instance.FirebaseServerKey);

            request.Content = new StringContent(jsonMessage, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                var task = client.SendAsync(request);
                var response = task.Result;
            }
        }

        public Task UnsubscribeAsync(string SubscriptionArn)
        {
            throw new System.NotImplementedException();
        }
    }
}