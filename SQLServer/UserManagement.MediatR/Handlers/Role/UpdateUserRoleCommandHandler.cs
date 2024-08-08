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

namespace UserManagement.MediatR.Handlers
{
    public class UpdateUserRoleCommandHandler(IUserRoleRepository userRoleRepository,
        IUnitOfWork<UserContext> uow,
        IMapper mapper) 
        : IRequestHandler<UpdateUserRoleCommand, ServiceResponse<UserRoleDto>>
    {
        public async Task<ServiceResponse<UserRoleDto>> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
        {
            var userRoles = await userRoleRepository.All.Where(c => c.RoleId == request.Id).ToListAsync(cancellationToken);
            var userRolesToAdd = request.UserRoles.Where(c => !userRoles.Select(c => c.UserId).Contains(c.UserId.Value)).ToList();
            userRoleRepository.AddRange(mapper.Map<List<UserRole>>(userRolesToAdd));
            var userRolesToDelete = userRoles.Where(c => !request.UserRoles.Select(cs => cs.UserId).Contains(c.UserId)).ToList();
            userRoleRepository.RemoveRange(userRolesToDelete);

            if (await uow.SaveAsync(cancellationToken) <= 0)
            {
                return ServiceResponse<UserRoleDto>.Return500();
            }
            return ServiceResponse<UserRoleDto>.ReturnSuccess();
        }
    }

}
