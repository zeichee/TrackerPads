using System.Collections.Generic;
using Oosa.Data;

namespace Vsslabs.Data
{
    public class Role : BaseEntity
    {
        public Role()
        {
            TableName = "Roles";
            RolePermissions = new List<RolePermission>();
        }

        public string RoleName { get; set; }
        public string Description { get; set; }

        public int? TenantId { get; set; }


        [Ignore]
        public IList<RolePermission> RolePermissions { get; set; }

    }
}