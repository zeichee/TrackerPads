using System.Collections.Generic;
using System.Threading.Tasks;
using Oosa.Paging;
using Oosa.Repository;
using Vsslabs.Data;

namespace Vsslabs.Dal.Contracts
{
    public interface IRoleRepository : IRepository<Role>
    {
        bool IsUnique(int roleId, string roleName);
        Task<bool> IsUniqueAsync(int roleId, string roleName);
        Task<IPagedList<Role>> SearchAsync(string filter, int page = 1, int pageSize = 25);
        Task<IEnumerable<Role>> GetRolesByUserIdAsync(int userId);
    }
}
