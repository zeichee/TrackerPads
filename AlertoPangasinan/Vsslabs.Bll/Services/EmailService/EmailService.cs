using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cirrus.Bll.Utilities;
using Oosa.Core;
using Oosa.Messaging;

namespace Vsslabs.Bll.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IEmailNotification _emailNotification;
        public EmailService(IEmailNotification emailNotification)
        {
            _emailNotification = emailNotification;

        }

        private Task Initialize()
        {
            throw new NotImplementedException();
        }

        public async Task<TaskResult> Validate()
        {
            //add validations here...
            try
            {
                await Initialize();
            }
            catch (Exception ex)
            {
                return TaskResult.Failed(new string[] { ex.Message });
            }

            return TaskResult.Success;
        }

        public async Task<bool> NotifyUser(Data.User user, string subject, string body)
        {
            await Initialize();

            _emailNotification.To = new[] { user.Email };
            _emailNotification.Subject = subject;
            _emailNotification.Message = body;
            _emailNotification.IsHtml = true;

            return (await _emailNotification.SendAsync()).Result;
        }


        public async Task<bool> NotifyUser(string subject, string body, params string[] to)
        {
            await Initialize();

            _emailNotification.To = to;
            _emailNotification.Subject = subject;
            _emailNotification.Message = body;
            _emailNotification.IsHtml = true;

            var result = await _emailNotification.SendAsync();

            return result.Result;
        }

        public async Task<TaskResult> TestSend(List<string> recipients)
        {
            await Initialize();

            _emailNotification.To = recipients.ToArray();
            _emailNotification.Subject = "This is a sample email";
            _emailNotification.Message = "***This is an automatically generated email. Please do not reply to this message.";
            _emailNotification.IsHtml = true;

            return (await _emailNotification.SendAsync());
        }
    }
}
