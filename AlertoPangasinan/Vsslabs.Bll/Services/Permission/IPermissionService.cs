using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vsslabs.Bll.Services.Permission
{
    public interface IPermissionService
    {
        Task<IList<Data.Permission>> GetAllAsync();
    }
}
