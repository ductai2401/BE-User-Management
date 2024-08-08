using AutoMapper;
using UserManagement.Data.Dto;
using UserManagement.MediatR.Queries;
using UserManagement.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Helper;
using Microsoft.Extensions.Logging;

namespace UserManagement.MediatR.Handlers
{
    public class GetUserQueryHandler(
        IUserRepository userRepository,
        IMapper mapper,
        ILogger<GetUserQueryHandler> logger
        ) : IRequestHandler<GetUserQuery, ServiceResponse<UserDto>>
    {
        public async Task<ServiceResponse<UserDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var entity = await userRepository
                .AllIncluding(c => c.UserRoles, cs => cs.UserClaims, ip => ip.UserAllowedIPs)
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
            if (entity != null)
                return ServiceResponse<UserDto>.ReturnResultWith200(mapper.Map<UserDto>(entity));
            else
            {
                logger.LogError("User not found");
                return ServiceResponse<UserDto>.ReturnFailed(404, "User not found");
            }
        }
    }
}
