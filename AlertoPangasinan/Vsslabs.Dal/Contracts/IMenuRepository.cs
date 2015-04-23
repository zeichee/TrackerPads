using System.Collections.Generic;
using System.Threading.Tasks;
using Oosa.Repository;
using Vsslabs.Data;

namespace Vsslabs.Dal.Contracts
{
    public interface IMenuRepository : IRepository<Menu>
    {
        Task<IList<Menu>> GetAllByUserId(int userId);
    }
}
