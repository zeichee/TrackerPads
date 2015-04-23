using Oosa.Data;

namespace Vsslabs.Data
{
    public class Permission : BaseEntity
    {
        public string ModuleAlias { get; set; }
        public string ModuleName { get; set; }
        public string Operation { get; set; }

    }
}