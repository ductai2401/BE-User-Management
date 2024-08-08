using UserManagement.Data.Dto;
using UserManagement.MediatR.Queries;
using UserManagement.Repository;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace UserManagement.MediatR.Handlers
{
    public class GetRoleUsersHandler(IUserRoleRepository userRoleRepository)
        : IRequestHandler<GetRoleUsersQuery, List<UserRoleDto>>
    {
        public async Task<List<UserRoleDto>> Handle(GetRoleUsersQuery request, CancellationToken cancellationToken)
        {
            var userRoles = await userRoleRepository
                .AllIncluding(c => c.User)
                .Where(c => c.RoleId == request.RoleId)
                .Select(cs => new UserRoleDto
                {
                    UserId = cs.UserId,
                    RoleId = cs.RoleId,
                    UserName = cs.User.UserName,
                    FirstName = cs.User.FirstName,
                    LastName = cs.User.LastName
                }).ToListAsync(cancellationToken);

            return userRoles;

        }
    }
}
