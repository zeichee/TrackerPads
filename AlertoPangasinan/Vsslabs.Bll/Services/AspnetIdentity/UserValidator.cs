using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Vsslabs.Bll.Services.AspnetIdentity
{
    public class UserValidator : UserValidator<Data.User, int>
    {
        private UserManager<Data.User, int> Manager { get; set; }
        public UserValidator(UserManager<Data.User, int> manager)
            : base(manager)
        {
            if (manager == null)
            {
                throw new ArgumentNullException("manager");
            }
            this.AllowOnlyAlphanumericUserNames = true;
            this.Manager = manager;
        }
        public override async Task<IdentityResult> ValidateAsync(Data.User item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            List<string> list = new List<string>();
            await this.ValidateUserName(item, list);
            if (this.RequireUniqueEmail)
            {
                await this.ValidateEmail(item, list);
            }
            IdentityResult result;
            if (list.Count > 0)
            {
                result = IdentityResult.Failed(list.ToArray());
            }
            else
            {
                result = IdentityResult.Success;
            }
            return result;
        }
        private async Task ValidateUserName(Data.User user, List<string> errors)
        {
            if (string.IsNullOrWhiteSpace(user.UserName))
            {
                errors.Add(string.Format(CultureInfo.CurrentCulture, "Username cannot be null or empty."));
            }
            else
            {
                if (this.AllowOnlyAlphanumericUserNames && !Regex.IsMatch(user.UserName, "^[A-Za-z0-9@_\\.]+$"))
                {
                    errors.Add(string.Format(CultureInfo.CurrentCulture, "Username should only contain Alphanumeric characters [Aa-Zz and 0-9]"));
                }
                else
                {
                    Data.User tUser = await this.Manager.FindByNameAsync(user.UserName);
                    if (tUser != null && !EqualityComparer<int>.Default.Equals(tUser.Id, user.Id))
                    {
                        errors.Add(string.Format(CultureInfo.CurrentCulture, "Username already exist.", user.UserName));
                    }
                }
            }
        }
        private async Task ValidateEmail(Data.User user, List<string> errors)
        {
            await Task.Factory.StartNew(() =>
             {
                 string text = user.Email;
                 if (string.IsNullOrWhiteSpace(text))
                 {
                     errors.Add(string.Format(CultureInfo.CurrentCulture, "Email cannot be null or empty."));
                 }
                 else
                 {
                     try
                     {
                         new MailAddress(text);
                     }
                     catch (FormatException)
                     {
                         errors.Add(string.Format(CultureInfo.CurrentCulture, "Email should be in valid format (e.g. jdelacruz@domain.com)"));
                         return;
                     }
                     Data.User tUser = this.Manager.FindByEmail(text);
                     //if (tUser != null && !EqualityComparer<int>.Default.Equals(tUser.Id, user.Id))
                     //{
                     //    errors.Add(string.Format(CultureInfo.CurrentCulture, "Email \"{0}\" already exists.", text));
                     //}
                 }
             });
        }
    }
}
