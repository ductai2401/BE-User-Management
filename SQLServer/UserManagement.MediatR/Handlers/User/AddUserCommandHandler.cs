using AutoMapper;
using UserManagement.Data;
using UserManagement.Data.Dto;
using UserManagement.MediatR.Commands;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Helper;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace UserManagement.MediatR.Handlers
{
    public class AddUserCommandHandler(
        IMapper mapper,
        UserManager<User> userManager,
        UserInfoToken userInfoToken,
        ILogger<AddUserCommandHandler> logger
        ) : IRequestHandler<AddUserCommand, ServiceResponse<UserDto>>
    {
        public async Task<ServiceResponse<UserDto>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var appUser = await userManager.FindByNameAsync(request.Email);
            if (appUser != null)
            {
                logger.LogError("Email already exist for another user.");
                return ServiceResponse<UserDto>.Return409("Email already exist for another user.");
            }
            var entity = mapper.Map<User>(request);
            entity.UserAllowedIPs = entity.UserAllowedIPs.DistinctBy(c => c.IPAddress).ToList();
            entity.CreatedBy = Guid.Parse(userInfoToken.Id);
            entity.ModifiedBy = Guid.Parse(userInfoToken.Id);
            entity.CreatedDate = DateTime.Now.ToLocalTime();
            entity.ModifiedDate = DateTime.Now.ToLocalTime();
            entity.Id = Guid.NewGuid();
            IdentityResult result = await userManager.CreateAsync(entity);
            if (!result.Succeeded)
            {
                return ServiceResponse<UserDto>.Return500();
            }
            if (!string.IsNullOrEmpty(request.Password))
            {
                string code = await userManager.GeneratePasswordResetTokenAsync(entity);
                IdentityResult passwordResult = await userManager.ResetPasswordAsync(entity, code, request.Password);
                if (!passwordResult.Succeeded)
                {
                    return ServiceResponse<UserDto>.Return500();
                }
            }
            return ServiceResponse<UserDto>.ReturnResultWith200(mapper.Map<UserDto>(entity));
        }
    }
}
