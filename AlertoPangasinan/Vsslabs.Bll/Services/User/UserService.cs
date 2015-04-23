using Microsoft.AspNet.Identity;
using Oosa.Core;
using Oosa.Paging;
using Oosa.Validation;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Vsslabs.Dal.Contracts;

namespace Vsslabs.Bll.Services.User
{
    /// <summary>
    /// This class 
    /// </summary>
    public class UserService : Disposable, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ApplicationUserManager _userManager;

        public UserService(IUserRepository userRepository, ApplicationUserManager userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        #region Commented. This methods can be accessed via ApplicationUserManager (IUserStore) (cont.)
        // -->>> Consider using ApplicationUserManager as there are already logic behind it especially in validations 

        //public int Log(Data.User user)
        //{
        //    Guard.ArgumentNotNull(user, "user");

        //    return _userRepository.Log(user);
        //}
        //public Task DeleteAsync(Data.User user)
        //{
        //    Guard.ArgumentNotNull(user, "user");
        //    return _userRepository.DeleteAsync(user);
        //}

        //public Task<int> CreateAsync(Data.User user)
        //{
        //    Guard.ArgumentNotNull(user, "user");

        //    return _userRepository.CreateAsync(user);
        //}
        //public Task UpdateAsync(Data.User user)
        //{
        //    Guard.ArgumentNotNull(user, "user");

        //    return _userRepository.UpdateAsync(user);
        //}
        #endregion

        public bool IsUnique(int userId, string username)
        {
            return _userRepository.IsUnique(userId, username);
        }

        public Task<IPagedList<Data.User>> SearchAsync(string filter, int page, int pageSize)
        {
            return _userRepository.SearchAsync(filter, page, pageSize);
        }

        public Task<IList<Data.User>> SearchAsync(string filter)
        {
            return _userRepository.SearchAsync(filter);
        }


        public Task<IList<Data.User>> GetAllAsync()
        {
            return _userRepository.GetAllRListAsync();
        }

        public Task<IList<Data.Permission>> GetUserPermissions(int userId)
        {
            return _userRepository.GetUserPermissionsAsync(userId);
        }

        public Task<Data.User> FindByIdAsync(int userId)
        {
            return _userRepository.GetByIdAsync(userId);
        }

        public async Task<IEnumerable<Data.User>> GetUsersById(int[] userIds)
        {
            Guard.ArgumentNotNull(userIds, "userIds");
            return await _userRepository.GetAllInIdsAsync(userIds);
        }

        public async Task<IdentityResult> DeleteUsers(int[] userIds, bool isConsumerPortal)
        {
            Guard.ArgumentNotNull(userIds, "userIds");
            var filterId = userIds.Where(i => i != 1001).ToArray();
            var users = GetUsersById(filterId);

            var errors = new List<string>();
            foreach (var user in await users)
            {
                if (isConsumerPortal && (user.IsTenantAdmin ?? false))
                {
                    errors.Add(string.Format("You cannot delete local tenant admin {0}.", user.UserName));
                    continue;
                }

                var r = await _userManager.DeleteAsync(user);
                if (r.Succeeded == false)
                    errors.AddRange(r.Errors);
            }

            return errors.Count > 0 ? IdentityResult.Failed(errors.ToArray()) : IdentityResult.Success;
        }


       
    }
}
