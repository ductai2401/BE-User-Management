using System;

namespace UserManagement.Data.Dto
{
    public class RoleClaimDto 
    {
        public int Id { get; set; }
        public Guid RoleId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public Guid ActionId { get; set; }
        public Guid PageId { get; set; }
    }
}
