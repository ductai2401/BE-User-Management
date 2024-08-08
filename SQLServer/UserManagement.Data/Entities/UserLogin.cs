using Microsoft.AspNetCore.Identity;
using System;

namespace UserManagement.Data
{
    public class UserLogin : IdentityUserLogin<Guid>
    {
        public virtual User User { get; set; }
    }
}
