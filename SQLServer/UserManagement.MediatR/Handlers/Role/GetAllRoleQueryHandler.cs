using AutoMapper;
using UserManagement.Data.Dto;
using UserManagement.MediatR.Queries;
using UserManagement.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace UserManagement.MediatR.Handlers
{
    public class GetAllRoleQueryHandler(
       IRoleRepository roleRepository,
        IMapper mapper
        ) : IRequestHandler<GetAllRoleQuery, List<RoleDto>>
    {
        public async Task<List<RoleDto>> Handle(GetAllRoleQuery request, CancellationToken cancellationToken)
        {
            var entities = await roleRepository.All.ToListAsync(cancellationToken);
            return mapper.Map<List<RoleDto>>(entities);
        }
    }
}
