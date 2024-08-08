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
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Helper;
using Microsoft.Extensions.Logging;

namespace UserManagement.MediatR.Handlers
{
    public class AddRoleCommandHandler(
       IRoleRepository roleRepository,
        IMapper mapper,
        IUnitOfWork<UserContext> uow,
        UserInfoToken userInfoToken,
        ILogger<AddRoleCommandHandler> logger
            ) : IRequestHandler<AddRoleCommand, ServiceResponse<RoleDto>>
    {
        public async Task<ServiceResponse<RoleDto>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await roleRepository.FindBy(c => c.Name == request.Name).FirstOrDefaultAsync(cancellationToken);
            if (entityExist != null)
            {
                logger.LogError("Role Name already exist.");
                return ServiceResponse<RoleDto>.Return409("Role Name already exist.");
            }
            var entity = mapper.Map<Role>(request);
            entity.Id = Guid.NewGuid();
            entity.CreatedBy = Guid.Parse(userInfoToken.Id);
            entity.CreatedDate = DateTime.Now.ToLocalTime();
            entity.ModifiedBy = Guid.Parse(userInfoToken.Id);
            entity.NormalizedName = entity.Name;
            roleRepository.Add(entity);
            if (await uow.SaveAsync(cancellationToken) <= 0)
            {
                return ServiceResponse<RoleDto>.Return500();
            }
            var entityDto = mapper.Map<RoleDto>(entity);
            return ServiceResponse<RoleDto>.ReturnResultWith200(entityDto); 
        }
    }
}
