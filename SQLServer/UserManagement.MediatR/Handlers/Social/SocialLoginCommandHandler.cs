using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Data;
using UserManagement.Data.Dto;
using UserManagement.Data.Dto.User;
using UserManagement.Helper;
using UserManagement.MediatR.Commands;
using UserManagement.Repository;

namespace UserManagement.MediatR.Handlers
{
    public class SocialLoginCommandHandler(
        IMapper mapper,
        UserManager<User> userManager,
        IUserRepository userRepository,
        ILoginAuditRepository loginAuditRepository,
        IHubContext<UserHub, IHubClient> hubContext,
        IRoleRepository roleRepository
        ) : IRequestHandler<SocialLoginCommand, ServiceResponse<UserAuthDto>>
    {
        public async Task<ServiceResponse<UserAuthDto>> Handle(SocialLoginCommand request, CancellationToken cancellationToken)
        {
            var loginAudit = new LoginAuditDto
            {
                UserName = request.UserName,
                RemoteIP = request.RemoteIp,
                Status = LoginStatus.Success.ToString(),
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                Provider = request.Provider
            };

            var appUser = await userManager.FindByNameAsync(request.Email);
            if (appUser != null)
            {
                appUser.FirstName = request.FirstName;
                appUser.LastName = request.LastName;
                await userManager.UpdateAsync(appUser);
            }
            else
            {
                var entity = mapper.Map<User>(request);
                entity.Id = Guid.NewGuid();
                entity.IsActive = true;

                // Assign Social medial Role to user
                var socialMediaRole = await roleRepository.All
                    .Where(c => c.Name.ToLower() == "social media")
                    .FirstOrDefaultAsync(cancellationToken);
                if (socialMediaRole != null)
                {
                    entity.UserRoles.Add(new UserRole
                    {
                        RoleId = socialMediaRole.Id,
                        UserId = entity.Id
                    });
                }

                IdentityResult result = await userManager.CreateAsync(entity);
                if (!result.Succeeded)
                {
                    loginAudit.Status = LoginStatus.Error.ToString();
                    await loginAuditRepository.LoginAudit(loginAudit);
                    return ServiceResponse<UserAuthDto>.Return500();
                }
                appUser = await userManager.FindByNameAsync(request.Email);
            }

            await loginAuditRepository.LoginAudit(loginAudit);
            var authUser = await userRepository.BuildUserAuthObject(appUser);
            var onlineUser = new SignlarUser
            {
                Email = authUser.Email,
                Id = authUser.Id.ToString()
            };
            await hubContext.Clients.All.Joined(onlineUser);
            return ServiceResponse<UserAuthDto>.ReturnResultWith200(authUser);
        }
    }
}
