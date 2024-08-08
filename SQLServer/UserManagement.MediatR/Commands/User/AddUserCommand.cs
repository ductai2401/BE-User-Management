using UserManagement.Data.Dto;
using MediatR;
using System.Collections.Generic;
using UserManagement.Helper;

namespace UserManagement.MediatR.Commands
{
    public class AddUserCommand : IRequest<ServiceResponse<UserDto>>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public string Address { get; set; }
        public List<UserAllowedIPDto> UserAllowedIPs { get; set; }
        public List<UserRoleDto> UserRoles { get; set; } = new List<UserRoleDto>();

    }
}
