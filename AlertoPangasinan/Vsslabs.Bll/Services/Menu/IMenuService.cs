using System.Collections.Generic;
using System.Threading.Tasks;
using Vsslabs.Data;
using MenuItem = Vsslabs.Data.Menu;

namespace Vsslabs.Bll.Services.Menu
{
    public interface IMenuService
    {
        Task<IList<MenuItem>> GetAllByUserId(int userId);
       
    }
}

