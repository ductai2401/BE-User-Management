using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Common.UnitOfWork;
using UserManagement.Data;
using UserManagement.Data.Dto;
using UserManagement.Domain;
using UserManagement.Helper;
using UserManagement.MediatR.Commands;

namespace UserManagement.MediatR.Handlers
{
    public class UpdateUserProfilePhotoCommandHandler(
        IMapper mapper,
        IUnitOfWork<UserContext> uow,
        UserInfoToken userInfoToken,
        UserManager<User> userManager,
        ILogger<UpdateUserProfileCommandHandler> logger,
        PathHelper pathHelper
        ) : IRequestHandler<UpdateUserProfilePhotoCommand, ServiceResponse<UserDto>>
    {
        public readonly PathHelper _pathHelper = pathHelper;

        public async Task<ServiceResponse<UserDto>> Handle(UpdateUserProfilePhotoCommand request, CancellationToken cancellationToken)
        {
            var filePath = $"{request.RootPath}/{_pathHelper.UserProfilePath}";
            var appUser = await userManager.FindByIdAsync(userInfoToken.Id);
            if (appUser == null)
            {
                logger.LogError("User does not exist.");
                return ServiceResponse<UserDto>.Return409("User does not exist.");
            }
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            // delete existing file
            if (!string.IsNullOrWhiteSpace(appUser.ProfilePhoto))
            {
                if (File.Exists($"{filePath}/{appUser.ProfilePhoto}"))
                {
                    File.Delete($"{filePath}/{appUser.ProfilePhoto}");
                }
            }

            // save new file
            if (request.FormFile.Any())
            {
                var profileFile = request.FormFile[0];
                var newProfilePhoto = $"{Guid.NewGuid()}{Path.GetExtension(profileFile.Name)}";
                string fullPath = Path.Combine(filePath, newProfilePhoto);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    profileFile.CopyTo(stream);
                }
                appUser.ProfilePhoto = newProfilePhoto;
            }
            else
            {
                appUser.ProfilePhoto = "";
            }

            // update user
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
