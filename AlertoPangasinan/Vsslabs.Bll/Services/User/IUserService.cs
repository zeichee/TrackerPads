using Microsoft.AspNet.Identity;
using Oosa.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vsslabs.Bll.Services.User
{
    public interface IUserService
    {
        bool IsUnique(int userId, string username);
        Task<IPagedList<Data.User>> SearchAsync(string filter, int page, int pageSize);
        Task<IList<Data.User>> SearchAsync(string filter);
        Task<IList<Data.User>> GetAllAsync();

        //int Log(Data.User user);
        //Task<int> CreateAsync(Data.User user);

        Task<IList<Data.Permission>> GetUserPermissions(int userId);

        //Task UpdateAsync(Data.User user);
        Task<Data.User> FindByIdAsync(int userId);
        //Task DeleteAsync(Data.User user);

        Task<IEnumerable<Data.User>> GetUsersById(int[] userIds);

        //Task<IdentityResult> DeleteUsers(int[] userIds);
        Task<IdentityResult> DeleteUsers(int[] filterId, bool isConsumerPortal);

    }
}
