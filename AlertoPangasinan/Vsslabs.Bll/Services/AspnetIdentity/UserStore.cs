using Vsslabs.Dal.Contracts;
using Microsoft.AspNet.Identity;
using Oosa.Core;
using Oosa.Ioc;
using Oosa.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Vsslabs.Bll.Services.AspnetIdentity
{
    /// <summary>
    /// Class that implements the key ASP.NET Identity user store iterfaces.
    /// </summary>
    public class UserStore : Disposable, IUserStore
    {
        private readonly IUserRepository _userRepository;
        public UserStore(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task CreateAsync(Data.User user)
        {
            Guard.ArgumentNotNull(user, "user");
            var userid = await _userRepository.CreateAsync(user);
            if (userid == 0)
                throw new Exception("Unable to save user");
        }

        public Task DeleteAsync(Data.User user)
        {
            Guard.ArgumentNotNull(user, "user");
            return _userRepository.DeleteAsync(user);
        }

        public Task<Data.User> FindByIdAsync(int userId)
        {
            return _userRepository.GetByIdAsync(userId);
        }

        public async Task<Data.User> FindByNameAsync(string userName)
        {
            Guard.ArgumentNotNull(userName, "userName");

            var user = await _userRepository.GetByUsernameAsync(userName);

            return user;
        }

        public Task UpdateAsync(Data.User user)
        {
            Guard.ArgumentNotNull(user, "user");

            return _userRepository.UpdateAsync(user);
        }

        public Task AddClaimAsync(Data.User user, Claim claim)
        {
            Guard.ArgumentNotNull(user, "user");
            Guard.ArgumentNotNull(claim, "claim");

            // todo : method here


            return Task.FromResult<object>(null);
        }

        public async Task<IList<Claim>> GetClaimsAsync(Data.User user)
        {
            Guard.ArgumentNotNull(user, "user");

            var claims = new List<Claim> {new Claim("TenantId", user.TenantId.ToString())};

            IList<Data.Permission> permissions = await _userRepository.GetUserPermissionsAsync(user.Id);
            claims.AddRange(permissions.Select(permission => new Claim(ClaimTypes.Role, permission.ModuleAlias)));

            return claims;
        }

        public Task RemoveClaimAsync(Data.User user, Claim claim)
        {
            Guard.ArgumentNotNull(user, "user");
            Guard.ArgumentNotNull(claim, "claim");

            // todo : method here

            return Task.FromResult<object>(null);
        }

        public Task AddLoginAsync(Data.User user, UserLoginInfo login)
        {
            Guard.ArgumentNotNull(user, "user");
            Guard.ArgumentNotNull(login, "login");

            // todo : method here

            return Task.FromResult<object>(null);
        }

        public Task<Data.User> FindAsync(UserLoginInfo login)
        {
            Guard.ArgumentNotNull(login, "login");

            return _userRepository.GetByLoginInfoAsync(login.LoginProvider, login.ProviderKey);
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(Data.User user)
        {
            Guard.ArgumentNotNull(user, "user");
            //todo : add method here
            return Task.FromResult<IList<UserLoginInfo>>(null);
        }

        public Task RemoveLoginAsync(Data.User user, UserLoginInfo login)
        {
            Guard.ArgumentNotNull(user, "user");
            Guard.ArgumentNotNull(login, "login");

            //todo : add method here

            return Task.FromResult<object>(null);
        }

        public Task AddToRoleAsync(Data.User user, string roleName)
        {
            Guard.ArgumentNotNull(user, "user");
            Guard.ArgumentNotNull(roleName, "roleName");

            //todo : add method here

            return Task.FromResult<object>(null);
        }

        public async Task<IList<string>> GetRolesAsync(Data.User user)
        {
            Guard.ArgumentNotNull(user, "user");

            var userPermissions = await _userRepository.GetUserPermissionsAsync(user.Id);

            return userPermissions.Select(m => m.ModuleAlias)
                .ToList();
        }

        public async Task<bool> IsInRoleAsync(Data.User user, string roleName)
        {
            Guard.ArgumentNotNull(user, "user");
            Guard.ArgumentNotNull(roleName, "roleName");

            var userPermissions = await _userRepository.GetUserPermissionsAsync(user.Id);

            return await Task.Run(() => userPermissions.Any(m => m.ModuleAlias == roleName));
        }

        public Task RemoveFromRoleAsync(Data.User user, string roleName)
        {
            //todo: add method here
            return Task.FromResult<object>(null);
        }

        public Task<string> GetPasswordHashAsync(Data.User user)
        {
            Guard.ArgumentNotNull(user, "user");

            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(Data.User user)
        {
            Guard.ArgumentNotNull(user, "user");

            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }

        public Task SetPasswordHashAsync(Data.User user, String passwordHash)
        {
            Guard.ArgumentNotNull(user, "user");

            user.PasswordHash = passwordHash;

            return Task.FromResult<object>(null);
        }

        public Task SetSecurityStampAsync(Data.User user, string stamp)
        {
            Guard.ArgumentNotNull(user, "user");

            user.SecurityStamp = stamp;

            return Task.FromResult<object>(null);
        }

        public Task<string> GetSecurityStampAsync(Data.User user)
        {
            Guard.ArgumentNotNull(user, "user");

            return Task.FromResult(user.SecurityStamp);
        }

        public IQueryable<Data.User> Users { get { return _userRepository.GetAll().AsQueryable(); } }
        public Task SetEmailAsync(Data.User user, string email)
        {
            Guard.ArgumentNotNull(user, "user");

            user.Email = email;

            return Task.FromResult<object>(null);
        }

        public Task<string> GetEmailAsync(Data.User user)
        {
            Guard.ArgumentNotNull(user, "user");

            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(Data.User user)
        {
            Guard.ArgumentNotNull(user, "user");

            return Task.FromResult(user.EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(Data.User user, bool confirmed)
        {
            Guard.ArgumentNotNull(user, "user");

            user.EmailConfirmed = confirmed;

            return Task.FromResult<object>(null);
        }

        public Task<Data.User> FindByEmailAsync(string email)
        {
            Guard.ArgumentNotNull(email, "email");

            return _userRepository.GetByEmailAsync(email);

        }

        public Task<Data.User> FindByEmailAndUserNameAsync(string username,string email)
        {
            Guard.ArgumentNotNull(email, "email");
            Guard.ArgumentNotNull(username, "username");

            return _userRepository.GetByEmailAndUsernameAsync(username,email);

        }

        public Task SetPhoneNumberAsync(Data.User user, string phoneNumber)
        {
            Guard.ArgumentNotNull(user, "user");

            user.PhoneNo = phoneNumber;

            return Task.FromResult<object>(null);
        }


        public Task<int> GetAccessFailedCountAsync(Data.User user)
        {
            Guard.ArgumentNotNull(user, "user");

            return Task.FromResult(user.AccessFailedCount);
        }

        public Task<bool> GetLockoutEnabledAsync(Data.User user)
        {
            Guard.ArgumentNotNull(user, "user");

            return Task.FromResult(user.LockoutEnabled);
        }

        public Task SetLockoutEnabledAsync(Data.User user, bool enabled)
        {
            Guard.ArgumentNotNull(user, "user");

            user.LockoutEnabled = enabled;

            return Task.FromResult(user.LockoutEnabled);
        }



        public Task<string> GetPhoneNumberAsync(Data.User user)
        {
            Guard.ArgumentNotNull(user, "user");
            return Task.FromResult(user.PhoneNo);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(Data.User user)
        {
            Guard.ArgumentNotNull(user, "user");
            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        public Task SetPhoneNumberConfirmedAsync(Data.User user, bool confirmed)
        {
            Guard.ArgumentNotNull(user, "user");
            user.PhoneNumberConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public Task<bool> GetTwoFactorEnabledAsync(Data.User user)
        {
            Guard.ArgumentNotNull(user, "user");
            return Task.FromResult(user.TwoFactorEnabled);
        }

        public Task SetTwoFactorEnabledAsync(Data.User user, bool enabled)
        {
            Guard.ArgumentNotNull(user, "user");
            user.TwoFactorEnabled = enabled;
            return Task.FromResult(0);
        }


        public Task<DateTimeOffset> GetLockoutEndDateAsync(Data.User user)
        {
            Guard.ArgumentNotNull(user, "user");
            return Task.FromResult(user.LockoutEndDateUtc.HasValue ? new DateTimeOffset(DateTime.SpecifyKind(user.LockoutEndDateUtc.Value, DateTimeKind.Utc)) : default(DateTimeOffset));
        }

        public Task<int> IncrementAccessFailedCountAsync(Data.User user)
        {
            Guard.ArgumentNotNull(user, "user");

            user.AccessFailedCount++;
            return Task.FromResult(user.AccessFailedCount);
        }

        public Task ResetAccessFailedCountAsync(Data.User user)
        {
            Guard.ArgumentNotNull(user, "user");

            user.AccessFailedCount = 0;
            return Task.FromResult(0);
        }

        public Task SetLockoutEndDateAsync(Data.User user, DateTimeOffset lockoutEnd)
        {
            Guard.ArgumentNotNull(user, "user");

            user.LockoutEndDateUtc =
                ((lockoutEnd == DateTimeOffset.MinValue) ? null : new DateTime?(lockoutEnd.UtcDateTime));

            return Task.FromResult(0);
        }

    }

}
