using UserManagement.Data.Dto;
using MediatR;
using System;
using UserManagement.Helper;

namespace UserManagement.MediatR.Commands
{
    public class DeleteRoleCommand : IRequest<ServiceResponse<RoleDto>>
    {
        public Guid Id { get; set; }
    }
}
