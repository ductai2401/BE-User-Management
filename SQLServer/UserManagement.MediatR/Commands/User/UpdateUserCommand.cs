using UserManagement.Data.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using UserManagement.Helper;

namespace UserManagement.MediatR.Commands
{
    public class UpdateUserCommand : IRequest<ServiceResponse<UserDto>>
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public string Address { get; set; }
        public List<UserAllowedIPDto> UserAllowedIPs { get; set; }
        public List<UserRoleDto> UserRoles { get; set; } = new List<UserRoleDto>();
    }
}
