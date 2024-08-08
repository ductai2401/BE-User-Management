using UserManagement.Data.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using UserManagement.Helper;

namespace UserManagement.MediatR.Commands
{
    public class UpdateUserRoleCommand : IRequest<ServiceResponse<UserRoleDto>>
    {
        public Guid Id { get; set; }
        public List<UserRoleDto> UserRoles { get; set; }
    }
}
