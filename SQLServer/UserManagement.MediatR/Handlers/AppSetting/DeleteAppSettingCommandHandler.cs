using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Common.UnitOfWork;
using UserManagement.Data.Dto;
using UserManagement.Domain;
using UserManagement.Helper;
using UserManagement.MediatR.Commands;
using UserManagement.Repository;

namespace UserManagement.MediatR.Handlers
{
    public class DeleteAppSettingCommandHandler(
        IAppSettingRepository appSettingRepository,
        IUnitOfWork<UserContext> uow,
        UserInfoToken userInfoToken,
        ILogger<DeleteAppSettingCommandHandler> logger
        ) : IRequestHandler<DeleteAppSettingCommand, ServiceResponse<AppSettingDto>>
    {
        public async Task<ServiceResponse<AppSettingDto>> Handle(DeleteAppSettingCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await appSettingRepository.FindAsync(request.Id, cancellationToken);
            if (entityExist == null)
            {
                logger.LogError("AppSetting Not Found.");
                return ServiceResponse<AppSettingDto>.Return404();
            }
            entityExist.IsDeleted = true;
            entityExist.DeletedBy = Guid.Parse(userInfoToken.Id);
            entityExist.DeletedDate = DateTime.Now.ToLocalTime();
            appSettingRepository.Update(entityExist);

            if (await uow.SaveAsync(cancellationToken) <= 0)
            {
                return ServiceResponse<AppSettingDto>.Return500();
            }
            return ServiceResponse<AppSettingDto>.ReturnResultWith204();
        }
    }
}
