using UserManagement.Data;
using UserManagement.Data.Dto;
using UserManagement.MediatR.Commands;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Helper;
using Microsoft.Extensions.Logging;

namespace UserManagement.MediatR.Handlers
{
    public class ResetPasswordCommandHandler(
        UserManager<User> userManager,
        ILogger<ResetPasswordCommandHandler> logger
        ) : IRequestHandler<ResetPasswordCommand, ServiceResponse<UserDto>>
    {
        public async Task<ServiceResponse<UserDto>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var entity = await userManager.FindByEmailAsync(request.UserName);
            if (entity == null)
            {
                logger.LogError("User not Found.");
                return ServiceResponse<UserDto>.ReturnFailed(404, "User not Found.");
            }
            string code = await userManager.GeneratePasswordResetTokenAsync(entity);
            IdentityResult passwordResult = await userManager.ResetPasswordAsync(entity, code, request.Password);
            if (!passwordResult.Succeeded)
            {
                return ServiceResponse<UserDto>.Return500();
            }
            return ServiceResponse<UserDto>.ReturnSuccess();
        }
    }
}
