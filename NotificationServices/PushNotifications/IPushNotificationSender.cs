using System.Threading.Tasks;
using NotificationServices.Models;

namespace NotificationServices.PushNotifications
{
    public interface IPushNotificationSender
    {
        Task<PushNotificationResponseDto> CreateAWSEndpointAsync(string Topic, string Token, string Platform);

        Task UnsubscribeAsync(string SubscriptionArn);

        Task SendAWSPushNotificationAsync(string TopicArn, SNS Payload);

        void SendFirebasePushNotification(string[] deviceTokens, string title, string body, object data);
    }
}
