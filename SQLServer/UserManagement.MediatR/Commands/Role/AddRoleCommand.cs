using UserManagement.Data.Dto;
using MediatR;
using System.Collections.Generic;
using UserManagement.Helper;

namespace UserManagement.MediatR.Commands
{
    public class AddRoleCommand : IRequest<ServiceResponse<RoleDto>>
    {
        public string Name { get; set; }
        public List<RoleClaimDto> RoleClaims { get; set; }
    }
}
