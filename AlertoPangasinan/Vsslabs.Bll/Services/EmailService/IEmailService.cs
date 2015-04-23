using Oosa.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cirrus.Bll.Utilities
{
    public interface IEmailService
    {
        Task<TaskResult> Validate();
        Task<bool> NotifyUser(string subject, string body, params string[] to);
        Task<TaskResult> TestSend(List<string> recipients);
    }
}
