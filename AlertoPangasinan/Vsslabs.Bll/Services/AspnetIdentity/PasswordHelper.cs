using Microsoft.AspNet.Identity;
using Oosa.Security;
using System;
using System.Text;

namespace Vsslabs.Bll.Services.AspnetIdentity
{
    /// <summary>
    /// A helper class to manage passwords
    /// </summary>
    public static class PasswordHelper
    {
        /// <summary>
        /// A simple random password generator.
        /// It generates password with characters from [a-z, A-Z, 0-9].
        /// </summary>
        /// <returns></returns>
        public static string GenerateRandomPwd()
        {
            var pwdChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            var numberChars = "1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            var i = 0;
            while (i < 14)
            {
                res.Append(pwdChars[rnd.Next(pwdChars.Length)]);
                i++;
            }
            res.Append(numberChars[rnd.Next(numberChars.Length)]); //just to be sure a number should be included for AD requirement

            return res.ToString();
        }

        /// <summary>
        /// An extension method that basically hashes strings.
        /// This is preferably used to hash passwords.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string HashForPassword(this string password)
        {
            var passwordHasher = new PasswordHasher();
            return passwordHasher.HashPassword(password);
        }


    }

    /// <summary>
    /// A class wrapper for Password hashing used in AspnetIdentity and or for hashing passwords
    /// </summary>
    public class PasswordHasher : DefaultCryptoEngine, IPasswordHasher
    {
        public string HashPassword(string password)
        {
            return base.HashPassword(password);
        }

        public new PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            var result = base.VerifyHashedPassword(hashedPassword, providedPassword);
            return result ? PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
        }
    }
}
