using System.Collections.Generic;
using Oosa.Data;

namespace Vsslabs.Data
{
    public class Menu : BaseEntity
    {
        public Menu()
        {
            Children = new List<Menu>();
        }

        public string ModuleName { get; set; }
        public int? ParentId { get; set; }
        public string DisplayName { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Area { get; set; }
        public string Icon { get; set; }

        [Ignore]
        public List<Menu> Children { get; set; }

        //[Ignore]
        //public Menu Parent { get; set; }
    }
}