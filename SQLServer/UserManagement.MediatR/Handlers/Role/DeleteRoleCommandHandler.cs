using UserManagement.Data.Dto;
using UserManagement.MediatR.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Repository;
using System;
using UserManagement.Domain;
using UserManagement.Common.UnitOfWork;
using UserManagement.Helper;
using Microsoft.Extensions.Logging;

namespace UserManagement.MediatR.Handlers
{
    public class DeleteRoleCommandHandler(
        UserInfoToken userInfoToken,
        IRoleRepository roleRepository,
        IUnitOfWork<UserContext> uow,
        ILogger<DeleteRoleCommandHandler> logger
        ) : IRequestHandler<DeleteRoleCommand, ServiceResponse<RoleDto>>
    {
        public async Task<ServiceResponse<RoleDto>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await roleRepository.FindAsync(request.Id, cancellationToken);
            if (entityExist == null)
            {
                logger.LogError("Not found");
                return ServiceResponse<RoleDto>.Return404();
            }
            entityExist.IsDeleted = true;
            entityExist.DeletedBy = Guid.Parse(userInfoToken.Id);
            entityExist.DeletedDate = DateTime.Now.ToLocalTime();
            roleRepository.Update(entityExist);
            if (await uow.SaveAsync(cancellationToken) <= 0)
            {
                return ServiceResponse<RoleDto>.Return500();
            }
            return ServiceResponse<RoleDto>.ReturnResultWith204();
        }
    }
}
