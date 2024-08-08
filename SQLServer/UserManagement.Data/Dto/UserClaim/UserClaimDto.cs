using System;

namespace UserManagement.Data.Dto
{
    public class UserClaimDto
    {
        public Guid? UserId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public Guid ActionId { get; set; }
        public Guid PageId { get; set; }
    }
}
