using System.Collections.Generic;
using System.Threading.Tasks;
using Vsslabs.Dal.Contracts;

namespace Vsslabs.Bll.Services.Permission
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;

        public PermissionService(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public Task<IList<Data.Permission>> GetAllAsync()
        {
            return _permissionRepository.GetAllRListAsync();
        }
    }
}