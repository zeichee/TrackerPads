using Cirrus.Bll.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Oosa.Core;
using Oosa.Ioc;
using System;
using System.Threading.Tasks;
using System.Linq;
using Vsslabs.Bll.Services.AspnetIdentity.Tasks;
using System.Globalization;

namespace Vsslabs.Bll.Services.AspnetIdentity
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    [System.Diagnostics.DebuggerStepThrough]
    public class ApplicationUserManager : UserManager<Data.User, int>
    {
        private IEmailService _emailService;


        public ApplicationUserManager(IUserStore store)
            : base(store)
        {
        }

        /// <summary>
        /// This method creates a new instance of ApplicationUserManager class 
        /// <para>This is used to register to OWIN context</para>
        /// </summary>
        /// <param name="options"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(Singleton<IContainer>.Instance.GetService<IUserStore>());
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 14,
                RequireNonLetterOrDigit = false,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };
            manager.PasswordHasher = new PasswordHasher();

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<Data.User, int>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<Data.User, int>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<Data.User, int>(dataProtectionProvider.Create("ASP.NET Identity"));
            }

            return manager;
        }

        /// <summary>
        /// Log new user
        /// </summary>
        /// <param name="user">The user model to create</param>
        /// <param name="isAd">Set to true if saving to AD should also be done; false, if saving only occurs in the local database.</param>
        /// <returns></returns>
        public async Task<TaskResult> CreateAsync(Data.User user, bool isAd)
        {
            var result = await ValidatePrerequisites();
            if (result.Succeeded == false)
                return result;

            var model = new UserTaskModel();
            model.User = user;
            model.User.Password = PasswordHelper.GenerateRandomPwd();

            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates the information of user.
        /// </summary>
        /// <param name="user">The user model to update</param>
        /// <param name="isAd">Set to true if update in AD should also be done; false, if update only occurs in the local database.</param>
        /// <returns></returns>
        public async Task<TaskResult> UpdateAsync(Data.User user, bool isAd)
        {
            var model = new UserTaskModel();
            model.User = user;

            return await new UpdateUserTask(model, this).ExecuteTask();
        }

        /// <summary>
        /// Deletes the user
        /// </summary>
        /// <param name="user">The user to be deleted</param>
        /// <param name="isAd">Set to true if deletion in AD should also be done; false, if deletion only occurs in the local database.</param>
        /// <returns></returns>
        public Task<TaskResult> DeleteAsync(Data.User user, bool isAd)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Change the password of user
        /// </summary>
        /// <param name="userId">The unique ID of the user</param>
        /// <param name="currentPassword">The old password</param>
        /// <param name="newPassword">The new password to be used</param>
        /// <param name="isAd">Set to true if update should also happen in AD</param>
        /// <returns></returns>
        public async Task<TaskResult> ChangePasswordAsync(int userId, string currentPassword, string newPassword, bool isAd)
        {
            var validatePwd = await this.PasswordValidator.ValidateAsync(newPassword);
            if (!validatePwd.Succeeded)
                return TaskResult.Failed(validatePwd.Errors.ToArray());

            var model = new UserTaskModel();
            model.User = await FindByIdAsync(userId);

            if (this.PasswordHasher.VerifyHashedPassword(model.User.PasswordHash, currentPassword) == PasswordVerificationResult.Failed)
                return TaskResult.Failed(new[] { string.Format(CultureInfo.CurrentCulture, "Incorrect old password.") });

            model.User.Password = newPassword;
            model.OldPassword = currentPassword;

            throw new NotImplementedException();
        }

        public async Task<TaskResult> ChangeForgottenPassword(string username,string email, bool isAd)
        {
            //verify email
            var validationResult = await ValidatePrerequisites();
            if (!validationResult.Succeeded)
            {
                return validationResult;
            }

            var user = await ((IUserStore)Store).FindByEmailAndUserNameAsync(username,email);
            if (user == null)
            {
                // Don't reveal that the user does not exist or is not confirmed
                return TaskResult.Failed(new[] { string.Format(CultureInfo.CurrentCulture, "Unable to verify your username or email address ") });
            }

            await this.RemovePasswordAsync(user.Id);

            var password = PasswordHelper.GenerateRandomPwd();

            string token = await this.GeneratePasswordResetTokenAsync(user.Id);
            var result = this.ResetPassword(user.Id, token, password);

            throw new NotImplementedException();

            return TaskResult.Failed(new[] { "Unable to complete request." });
        }

        public Task<TaskResult> ValidatePrerequisites()
        {
            throw new NotImplementedException();
        }
    }



}
