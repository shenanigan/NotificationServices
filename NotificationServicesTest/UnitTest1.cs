using System;
using NotificationServices;
using NotificationServices.PushNotifications;
using Xunit;

namespace NotificationServicesTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Configuration.Instance.FirebaseServerKey = "AAAArjSW52w:APA91bF1zSbuuMVfh-HwO5OuBF_ADnH-HyNZ-FWaJFiWGxoS-1tfaoviMIRpByEIjdRXFlZ4-SE3j2n9lIVxjjp33gwgzdu810G3YnzWm28YFJw7pUdKuKHPtgq693zb-ySAWt5nevJl";
            string[] tokens = new string[1];
            tokens[0] = "eOSCsCdslxA:APA91bHHMRoZ2bvHUXThY37ZIKd3Hy8U40otEn0XKTg7EorgOi9cK65Nz1BhuUqyWjIe7adfH4OX5Ocob_1P2i3EwhWuWTGFZ9k0lPLMyzBYAA_HmYoA4X7r_0mpKeapC13NSLBvkRei";
            PushNotificationSenderFactory.GetFirebasePushNotificationSender().SendFirebasePushNotification(tokens, "Hello Nagesh", "Hello Text", new { Text = "Success" });
        }
    }
}
