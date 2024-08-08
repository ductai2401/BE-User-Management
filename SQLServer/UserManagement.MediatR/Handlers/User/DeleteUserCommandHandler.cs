using AutoMapper;
using UserManagement.Data;
using UserManagement.Data.Dto;
using UserManagement.MediatR.Commands;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;
using System;
using UserManagement.Helper;
using Microsoft.Extensions.Logging;

namespace UserManagement.MediatR.Handlers
{
    public class DeleteUserCommandHandler(
        UserManager<User> userManager,
        IMapper mapper,
        UserInfoToken userInfoToken,
        ILogger<DeleteUserCommandHandler> logger
        ) : IRequestHandler<DeleteUserCommand, ServiceResponse<UserDto>>
    {
        public async Task<ServiceResponse<UserDto>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var appUser = await userManager.FindByIdAsync(request.Id.ToString());
            if (appUser == null)
            {
                logger.LogError("User does not exist.");
                return ServiceResponse<UserDto>.Return409("User does not exist.");
            }
            appUser.IsDeleted = true;
            appUser.DeletedDate = DateTime.Now.ToLocalTime();
            appUser.DeletedBy = Guid.Parse(userInfoToken.Id);
            IdentityResult result = await userManager.UpdateAsync(appUser);
            if (!result.Succeeded)
            {
                return ServiceResponse<UserDto>.Return500();
            }
            return ServiceResponse<UserDto>.ReturnResultWith200(mapper.Map<UserDto>(appUser));
        }
    }
}
