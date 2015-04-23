using Oosa.Data;

namespace Vsslabs.Data
{
    public class RolePermission : BaseEntity
    {
        public int RoleId { get; set; }
        public int PermissionId { get; set; }

        [Ignore]
        public Role Role { get; set; }

        [Ignore]
        public Permission Permission { get; set; }
    }
}