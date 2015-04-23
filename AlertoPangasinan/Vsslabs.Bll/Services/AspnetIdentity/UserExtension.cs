using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Vsslabs.Bll.Services.AspnetIdentity
{
    public static class UserExtension
    {
        public static async Task<ClaimsIdentity> GenerateUserIdentityAsync(this Data.User user, UserManager<Data.User, int> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here

            return userIdentity;
        }

       
    }
}
