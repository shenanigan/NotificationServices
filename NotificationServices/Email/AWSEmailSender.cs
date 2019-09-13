using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon;
using Amazon.Runtime;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;

namespace NotificationServices.Email
{
    class AWSEmailSender : IEmailSender
    {
        private readonly AmazonSimpleEmailServiceClient _sesClient;

       internal AWSEmailSender()
        {
            var awsCredentials = new BasicAWSCredentials(Configuration.Instance.AWSAccessKey, Configuration.Instance.AWSSecretKey);
            _sesClient = new AmazonSimpleEmailServiceClient(awsCredentials, RegionEndpoint.GetBySystemName(Configuration.Instance.AWSRegionEndPoint));
        }

        public virtual async Task SendAWSEmailAsync(string Source, string ToEmail, string TemplateName, string TemplateData)
        {
            if(Configuration.Instance.AWSAccessKey == null ||
                Configuration.Instance.AWSSecretKey == null ||
                Configuration.Instance.AWSRegionEndPoint == null)
            {
                throw new System.Exception("Initiate the configuration values for AWS via Configuration.Instance");
            }
            
            var request = new SendTemplatedEmailRequest
            {
                Source = Source,
                Template = TemplateName,
                Destination = new Destination(new List<string> { ToEmail }),
                TemplateData = TemplateData
            };

            var response = await _sesClient.SendTemplatedEmailAsync(request);
            if(response.HttpStatusCode != System.Net.HttpStatusCode.OK) 
            {
                throw new System.Exception(response.ToString());
            }
        }
    }
}
