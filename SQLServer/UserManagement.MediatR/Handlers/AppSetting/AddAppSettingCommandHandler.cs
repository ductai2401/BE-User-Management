using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Common.UnitOfWork;
using UserManagement.Data;
using UserManagement.Data.Dto;
using UserManagement.Domain;
using UserManagement.Helper;
using UserManagement.MediatR.Commands;
using UserManagement.Repository;

namespace UserManagement.MediatR.Handlers
{
    public class AddAppSettingCommandHandler(
        IAppSettingRepository appSettingRepository,
        IMapper mapper,
        IUnitOfWork<UserContext> uow,
        UserInfoToken userInfoToken,
        ILogger<AddAppSettingCommandHandler> logger
        ) : IRequestHandler<AddAppSettingCommand, ServiceResponse<AppSettingDto>>
    {
        public async Task<ServiceResponse<AppSettingDto>> Handle(AddAppSettingCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await appSettingRepository.FindBy(c => c.Key == request.Key).FirstOrDefaultAsync(cancellationToken);
            if (entityExist != null)
            {
                logger.LogError("AppSetting already exist.");
                return ServiceResponse<AppSettingDto>.Return409("App Setting already exist.");
            }

            var entity = mapper.Map<AppSetting>(request);
            entity.Id = Guid.NewGuid();
            entity.CreatedBy = Guid.Parse(userInfoToken.Id);
            entity.CreatedDate = DateTime.Now.ToLocalTime();
            entity.ModifiedBy = Guid.Parse(userInfoToken.Id);
            appSettingRepository.Add(entity);
            
            if (await uow.SaveAsync(cancellationToken) <= 0)
            {
                return ServiceResponse<AppSettingDto>.Return500();
            }

            var entityDto = mapper.Map<AppSettingDto>(entity);
            return ServiceResponse<AppSettingDto>.ReturnResultWith200(entityDto);
        }
    }
}
