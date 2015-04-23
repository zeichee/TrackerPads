using System.Collections.Generic;
using System.Threading.Tasks;
using Vsslabs.Dal.Contracts;
using MenuItem = Vsslabs.Data.Menu;

namespace Vsslabs.Bll.Services.Menu
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _menuRepository;

        public MenuService(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public Task<IList<MenuItem>> GetAllByUserId(int userId)
        {
            return _menuRepository.GetAllByUserId(userId);
        }
    }
}
