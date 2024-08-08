using AutoMapper;
using UserManagement.Common.UnitOfWork;
using UserManagement.Data;
using UserManagement.Data.Dto;
using UserManagement.Domain;
using UserManagement.MediatR.Commands;
using UserManagement.Repository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Helper;
using Microsoft.Extensions.Logging;

namespace UserManagement.MediatR.Handlers
{
    public class UpdateUserCommandHandler(
        IUserRoleRepository userRoleRepository,
        IMapper mapper,
        IUnitOfWork<UserContext> uow,
        UserManager<User> userManager,
        UserInfoToken userInfoToken,
        IUserAllowedIPRepository userAllowedIPRepository,
        ILogger<UpdateUserCommandHandler> logger
        ) : IRequestHandler<UpdateUserCommand, ServiceResponse<UserDto>>
    {
        public async Task<ServiceResponse<UserDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var appUser = await userManager.FindByIdAsync(request.Id.ToString());
            if (appUser == null)
            {
                logger.LogError("User does not exist.");
                return ServiceResponse<UserDto>.Return409("User does not exist.");
            }
            appUser.FirstName = request.FirstName;
            appUser.LastName = request.LastName;
            appUser.PhoneNumber = request.PhoneNumber;
            appUser.Address = request.Address;
            appUser.IsActive = request.IsActive;
            appUser.ModifiedDate = DateTime.Now.ToLocalTime();
            appUser.ModifiedBy = Guid.Parse(userInfoToken.Id);
            IdentityResult result = await userManager.UpdateAsync(appUser);

            // Update User's Role
            var userRoles = userRoleRepository.All.Where(c => c.UserId == appUser.Id).ToList();
            var rolesToAdd = request.UserRoles.Where(c => !userRoles.Select(c => c.RoleId).Contains(c.RoleId)).ToList();
            userRoleRepository.AddRange(mapper.Map<List<UserRole>>(rolesToAdd));
            var rolesToDelete = userRoles.Where(c => !request.UserRoles.Select(cs => cs.RoleId).Contains(c.RoleId)).ToList();
            userRoleRepository.RemoveRange(rolesToDelete);

            // Update User's Allowed IPs
            var userAllowedIPs = userAllowedIPRepository.All.Where(c => c.UserId == appUser.Id).ToList();
            var ipsToAdd = request.UserAllowedIPs
                .Where(c => !userAllowedIPs.Select(c => c.IPAddress).Contains(c.IPAddress))
                .Select(cs => new UserAllowedIP
                {
                    IPAddress = cs.IPAddress,
                    UserId = appUser.Id
                })
                .DistinctBy(c => c.IPAddress)
                .ToList();

            userAllowedIPRepository.AddRange(ipsToAdd);
            var ipsToDelete = userAllowedIPs
                .Where(c => !request.UserAllowedIPs.Select(cs => cs.IPAddress).Contains(c.IPAddress))
                .ToList();
            userAllowedIPRepository.RemoveRange(ipsToDelete);

            if (await uow.SaveAsync(cancellationToken) <= 0 && !result.Succeeded)
            {
                return ServiceResponse<UserDto>.Return500();
            }
            return ServiceResponse<UserDto>.ReturnResultWith200(mapper.Map<UserDto>(appUser));
        }
    }
}
