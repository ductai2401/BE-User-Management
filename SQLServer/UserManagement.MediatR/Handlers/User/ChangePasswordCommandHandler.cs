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
    public class ChangePasswordCommandHandler(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        ILogger<ChangePasswordCommandHandler> logger
        ) : IRequestHandler<ChangePasswordCommand, ServiceResponse<UserDto>>
    {
        public async Task<ServiceResponse<UserDto>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await signInManager.PasswordSignInAsync(request.UserName, request.OldPassword, false, false);
            if (!result.Succeeded)
            {
                logger.LogError("Old Password does not match.");
                return ServiceResponse<UserDto>.ReturnFailed(422, "Old Password does not match.");
            }

            var entity = await userManager.FindByNameAsync(request.UserName);
            string code = await userManager.GeneratePasswordResetTokenAsync(entity);
            IdentityResult passwordResult = await userManager.ResetPasswordAsync(entity, code, request.NewPassword);
            if (!passwordResult.Succeeded)
            {
                return ServiceResponse<UserDto>.Return500();
            }
            return ServiceResponse<UserDto>.ReturnSuccess();
        }
    }
}
