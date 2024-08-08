using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
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
    public class AddEmailSMTPSettingCommandHandler(
        IEmailSMTPSettingRepository emailSMTPSettingRepository,
        IMapper mapper,
        IUnitOfWork<UserContext> uow
        ) : IRequestHandler<AddEmailSMTPSettingCommand, ServiceResponse<EmailSMTPSettingDto>>
    {
        public async Task<ServiceResponse<EmailSMTPSettingDto>> Handle(AddEmailSMTPSettingCommand request, CancellationToken cancellationToken)
        {
            var entity = mapper.Map<EmailSMTPSetting>(request);
            emailSMTPSettingRepository.Add(entity);

            // remove other as default
            if (entity.IsDefault)
            {
                var defaultEmailSMTPSettings = await emailSMTPSettingRepository.All
                    .Where(c => c.IsDefault).ToListAsync(cancellationToken);
                defaultEmailSMTPSettings.ForEach(c => c.IsDefault = false);
                emailSMTPSettingRepository.UpdateRange(defaultEmailSMTPSettings);
            }

            if (await uow.SaveAsync(cancellationToken) <= 0)
            {
                return ServiceResponse<EmailSMTPSettingDto>.Return500();
            }

            var entityDto = mapper.Map<EmailSMTPSettingDto>(entity);
            return ServiceResponse<EmailSMTPSettingDto>.ReturnResultWith200(entityDto);
        }
    }
}
