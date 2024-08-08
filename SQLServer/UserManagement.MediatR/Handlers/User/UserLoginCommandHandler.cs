using UserManagement.Data;
using UserManagement.Data.Dto;
using UserManagement.MediatR.Commands;
using UserManagement.Repository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Helper;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using UserManagement.Data.Dto.User;

namespace UserManagement.MediatR.Handlers
{
    public class UserLoginCommandHandler(
        IUserRepository userRepository,
        SignInManager<User> signInManager,
        ILoginAuditRepository loginAuditRepository,
        IHubContext<UserHub, IHubClient> hubContext
        ) : IRequestHandler<UserLoginCommand, ServiceResponse<UserAuthDto>>
    {
        public async Task<ServiceResponse<UserAuthDto>> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            var loginAudit = new LoginAuditDto
            {
                UserName = request.UserName,
                RemoteIP = request.RemoteIp,
                Status = LoginStatus.Error.ToString(),
                Latitude = request.Latitude,
                Longitude = request.Longitude
            };
            var result = await signInManager.PasswordSignInAsync(request.UserName, request.Password, false, false);
            if (result.Succeeded)
            {
                var userInfo = await userRepository
                    .AllIncluding(c => c.UserAllowedIPs)
                    .Where(c => c.UserName == request.UserName)
                    .FirstOrDefaultAsync(cancellationToken);
                if (!userInfo.IsActive)
                {
                    await loginAuditRepository.LoginAudit(loginAudit);
                    return ServiceResponse<UserAuthDto>.ReturnFailed(401, "UserName Or Password is InCorrect.");
                }

                if (userInfo.UserAllowedIPs.Count > 0 && !userInfo.UserAllowedIPs.Any(c => c.IPAddress == request.RemoteIp))
                {
                    await loginAuditRepository.LoginAudit(loginAudit);
                    return ServiceResponse<UserAuthDto>.ReturnFailed(401, "You don't have access on this IP Address.");
                }
                loginAudit.Status = LoginStatus.Success.ToString();
                await loginAuditRepository.LoginAudit(loginAudit);
                var authUser = await userRepository.BuildUserAuthObject(userInfo);
                var onlineUser = new SignlarUser
                {
                    Email = authUser.Email,
                    Id = authUser.Id.ToString()
                };
                await hubContext.Clients.All.Joined(onlineUser);
                return ServiceResponse<UserAuthDto>.ReturnResultWith200(authUser);
            }
            else
            {
                await loginAuditRepository.LoginAudit(loginAudit);
                return ServiceResponse<UserAuthDto>.ReturnFailed(401, "UserName Or Password is InCorrect.");
            }
        }
    }
}
