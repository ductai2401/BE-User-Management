using UserManagement.Data.Dto;
using MediatR;
using System;
using System.Collections.Generic;

namespace UserManagement.MediatR.Queries
{
    public class GetRoleUsersQuery : IRequest<List<UserRoleDto>>
    {
        public Guid RoleId { get; set; }
    }
}
