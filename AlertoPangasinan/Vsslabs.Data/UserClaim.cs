using Oosa.Data;

namespace Vsslabs.Data
{
    public class UserClaim : BaseEntity
    {
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public int UserId { get; set; }
    }
}