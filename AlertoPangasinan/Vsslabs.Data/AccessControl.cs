using Oosa.Data;

namespace Vsslabs.Data
{
    public class AccessControl : BaseEntity
    {
        public AccessControl()
        {
            TableName = "AccessControl";
        }

        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}