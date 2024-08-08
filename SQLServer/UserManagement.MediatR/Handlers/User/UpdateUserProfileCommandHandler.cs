using AutoMapper;
using UserManagement.Common.UnitOfWork;
using UserManagement.Data;
using UserManagement.Data.Dto;
using UserManagement.Domain;
using UserManagement.MediatR.Commands;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Helper;
using Microsoft.Extensions.Logging;

namespace UserManagement.MediatR.Handlers
{
    public class UpdateUserProfileCommandHandler(
        IMapper mapper,
        IUnitOfWork<UserContext> uow,
        UserInfoToken userInfoToken,
        UserManager<User> userManager,
        ILogger<UpdateUserProfileCommandHandler> logger,
        PathHelper pathHelper
        ) : IRequestHandler<UpdateUserProfileCommand, ServiceResponse<UserDto>>
    {
        public readonly PathHelper _pathHelper = pathHelper;

        public async Task<ServiceResponse<UserDto>> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
        {
            var appUser = await userManager.FindByIdAsync(userInfoToken.Id);
            if (appUser == null)
            {
                logger.LogError("User does not exist.");
                return ServiceResponse<UserDto>.Return409("User does not exist.");
            }
            appUser.FirstName = request.FirstName;
            appUser.LastName = request.LastName;
            appUser.PhoneNumber = request.PhoneNumber;
            appUser.Address = request.Address;
            IdentityResult result = await userManager.UpdateAsync(appUser);
            if (await uow.SaveAsync(cancellationToken) <= 0 && !result.Succeeded)
            {
                return ServiceResponse<UserDto>.Return500();
            }
            if (!string.IsNullOrWhiteSpace(appUser.ProfilePhoto))
                appUser.ProfilePhoto = $"{_pathHelper.UserProfilePath}/{appUser.ProfilePhoto}";
            return ServiceResponse<UserDto>.ReturnResultWith200(mapper.Map<UserDto>(appUser));
        }
    }

}
