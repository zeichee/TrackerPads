using System;
using Microsoft.AspNet.Identity;
using Oosa.Data;

namespace Vsslabs.Data
{
    public class User : BaseEntity, IUser<int>
    {
        public User()
        {
            TableName = "Users";
        }

        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string MobileNo { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int? TenantId { get; set; }
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public bool EmailConfirmed { get; set; }
        public string SecurityStamp { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public bool? IsTenantAdmin { get; set; }

        [Ignore]
        public string Password { get; set; }


        [Ignore]
        public Role Role { get; set; }

        public string UserName { get; set; }
    }
}