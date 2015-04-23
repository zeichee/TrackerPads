using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace Vsslabs.Bll.Services.AspnetIdentity
{
    public interface IUserStore :
                            IUserClaimStore<Data.User, int>,
                            IUserLoginStore<Data.User, int>,
                            IUserRoleStore<Data.User, int>,
                            IUserPasswordStore<Data.User, int>,
                            IUserSecurityStampStore<Data.User, int>,
                            IQueryableUserStore<Data.User, int>,
                            IUserEmailStore<Data.User, int>,
                            IUserPhoneNumberStore<Data.User, int>,
                            IUserTwoFactorStore<Data.User, int>,
                            IUserLockoutStore<Data.User, int>
    {
        Task<Data.User> FindByEmailAndUserNameAsync(string username, string email);
    }
}
