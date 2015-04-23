using System.Threading.Tasks;
using Vsslabs.Dal.Contracts;
using Oosa.Paging;
using System.Collections.Generic;

namespace Vsslabs.Bll.Services.Role
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public Task<Data.Role> GetByIdAsync(int roleId)
        {
            return _roleRepository.GetByIdAsync(roleId);
        }

        public bool CheckIfUniqueRole(int id, string name)
        {
            return _roleRepository.IsUnique(id, name);
        }

        public async Task<bool> CreateAsync(Data.Role role)
        {
            if (_roleRepository.IsUnique(role.Id, role.RoleName))
                return (await _roleRepository.CreateAsync(role)) > 0;

            return false;
        }

        public async Task<bool> DeleteAsync(int roleId)
        {
            return await _roleRepository.DeleteAsync(roleId) > 0;
        }

        public async  Task<IPagedList<Data.Role>> SearchAsync(string filter, int page, int pageSize)
        {
            var roles = await _roleRepository.SearchAsync(filter, page, pageSize);

            return roles;
        }


        public async Task<IEnumerable<Data.Role>> GetRolesByUserIdAsync(int userId)
        {
            return (await _roleRepository.GetRolesByUserIdAsync(userId));
        }


        public async Task<IEnumerable<Data.Role>> GetAllAsync()
        {
            return await _roleRepository.GetAllAsync();
        }
    }
}