using Microsoft.AspNetCore.Identity;
using System;

namespace UserManagement.Data
{
    public class UserClaim : IdentityUserClaim<Guid>
    {
        public Guid ActionId { get; set; }
        public Guid PageId { get; set; }
        public virtual User User { get; set; }
        public Action Action { get; set; }
        public Page Page { get; set; }
    }
}
