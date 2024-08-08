using AutoMapper;
using UserManagement.Common.UnitOfWork;
using UserManagement.Data;
using UserManagement.Data.Dto;
using UserManagement.Domain;
using UserManagement.MediatR.Commands;
using UserManagement.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Helper;
using Microsoft.Extensions.Logging;

namespace UserManagement.MediatR.Handlers
{
    public class UpdateRoleCommandHandler(
        IRoleRepository roleRepository,
        IRoleClaimRepository roleClaimRepository,
        IMapper mapper,
        IUnitOfWork<UserContext> uow,
        UserInfoToken userInfoToken,
        ILogger<UpdateRoleCommandHandler> logger
        ) : IRequestHandler<UpdateRoleCommand, ServiceResponse<RoleDto>>
    {
        public async Task<ServiceResponse<RoleDto>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await roleRepository.FindBy(c => c.Name == request.Name && c.Id != request.Id)
                 .FirstOrDefaultAsync(cancellationToken);
            if (entityExist != null)
            {
                logger.LogError("Role Name Already Exist.");
                return ServiceResponse<RoleDto>.Return409("Role Name Already Exist.");
            }

            // Update Role
            entityExist = await roleRepository
                .FindByInclude(v => v.Id == request.Id, c => c.RoleClaims)
                .FirstOrDefaultAsync(cancellationToken);
            entityExist.Name = request.Name;
            entityExist.ModifiedBy = Guid.Parse(userInfoToken.Id);
            entityExist.ModifiedDate = DateTime.Now.ToLocalTime();
            entityExist.NormalizedName = request.Name;
            roleRepository.Update(entityExist);

            // update Role Claim
            var roleClaims = entityExist.RoleClaims.ToList();
            var roleClaimsToAdd = request.RoleClaims.Where(c => !roleClaims.Select(c => c.Id).Contains(c.Id)).ToList();
            roleClaimRepository.AddRange(mapper.Map<List<RoleClaim>>(roleClaimsToAdd));
            var roleClaimsToDelete = roleClaims.Where(c => !request.RoleClaims.Select(cs => cs.Id).Contains(c.Id)).ToList();
            roleClaimRepository.RemoveRange(roleClaimsToDelete);

            // TODO: update user Role
            if (await uow.SaveAsync(cancellationToken) <= 0)
            {
                return ServiceResponse<RoleDto>.Return500();
            }
            return ServiceResponse<RoleDto>.ReturnResultWith200(mapper.Map<RoleDto>(entityExist));
        }
    }
}
