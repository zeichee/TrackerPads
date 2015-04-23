using System.Threading.Tasks;
using Oosa.Paging;
using System.Collections.Generic;

namespace Vsslabs.Bll.Services.Role
{
    public interface IRoleService
    {
        Task<Data.Role> GetByIdAsync(int roleId);
        bool CheckIfUniqueRole(int id, string name);
        Task<bool> CreateAsync(Data.Role role);

        Task<bool> DeleteAsync(int roleId);
        Task<IPagedList<Data.Role>> SearchAsync(string filter, int page, int pageSize);
        Task<IEnumerable<Data.Role>> GetRolesByUserIdAsync(int userId);
        Task<IEnumerable<Data.Role>> GetAllAsync();
    }
}
