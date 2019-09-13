using System;
using System.Threading.Tasks;

namespace NotificationServices.SMS
{
    public interface ISMSSender
    {
        Task SendSmsAsync(string number, string message, string SenderId = "Notif", string SMSType = "Transactional");
    }
}
