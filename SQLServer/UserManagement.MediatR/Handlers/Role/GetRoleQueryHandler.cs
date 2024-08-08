using AutoMapper;
using UserManagement.Data.Dto;
using UserManagement.MediatR.Queries;
using UserManagement.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using UserManagement.Helper;

namespace UserManagement.MediatR.Handlers
{
    public class GetRoleQueryHandler(
       IRoleRepository roleRepository,
        IMapper mapper,
        ILogger<GetRoleQueryHandler> logger) : IRequestHandler<GetRoleQuery, ServiceResponse<RoleDto>>
    {
        public async Task<ServiceResponse<RoleDto>> Handle(GetRoleQuery request, CancellationToken cancellationToken)
        {
            var entity = await roleRepository.AllIncluding(c => c.UserRoles, cs => cs.RoleClaims)
                .Where(c => c.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (entity != null)
                return ServiceResponse<RoleDto>.ReturnResultWith200(mapper.Map<RoleDto>(entity));
            else
            {
                logger.LogError("Not found");
                return ServiceResponse<RoleDto>.Return404();
            }
        }
    }
}
