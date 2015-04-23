using System.Collections.Generic;
using System.Threading.Tasks;
using Oosa.Paging;
using Oosa.Repository;
using Vsslabs.Data;

namespace Vsslabs.Dal.Contracts
{
    public interface IUserRepository : IRepository<User>
    {
        bool IsUnique(int userId, string username);
        Task<IPagedList<User>> SearchAsync(string filter, int page = 1, int pageSize = 25);
        Task<IList<User>> SearchAsync(string filter);
        Task<User> GetByLoginInfoAsync(string loginProvider, string providerKey);
        Task<IList<Permission>> GetUserPermissionsAsync(int userId);
        Task<User> GetByUsernameAsync(string userName);
        Task<User> GetByEmailAsync(string email);

        Task<User> GetByEmailAndUsernameAsync(string username,string email);
        Task<IEnumerable<User>> GetAllInIdsAsync(int[] userIds);
        Task<IEnumerable<User>> GetAllByRoleAsync(int roleId);
    }
}
