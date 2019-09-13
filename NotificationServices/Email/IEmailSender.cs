using System.Threading.Tasks;

namespace NotificationServices.Email
{
    public interface IEmailSender
    {
        Task SendAWSEmailAsync(string Source, string ToEmail, string TemplateName, string TemplateData);
    }
}