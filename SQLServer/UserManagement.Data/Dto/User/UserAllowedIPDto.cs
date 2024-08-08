using System;

namespace UserManagement.Data.Dto
{
    public class UserAllowedIPDto
    {
        public Guid? UserId { get; set; }
        public string IPAddress { get; set; }
    }
}
