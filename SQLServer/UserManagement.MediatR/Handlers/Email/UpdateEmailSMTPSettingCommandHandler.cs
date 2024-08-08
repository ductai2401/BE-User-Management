using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
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
    public class UpdateEmailSMTPSettingCommandHandler(
        IEmailSMTPSettingRepository emailSMTPSettingRepository,
        IMapper mapper,
        IUnitOfWork<UserContext> uow,
        ILogger<AddEmailSMTPSettingCommand> logger
        ) : IRequestHandler<UpdateEmailSMTPSettingCommand, ServiceResponse<EmailSMTPSettingDto>>
    {
        public async Task<ServiceResponse<EmailSMTPSettingDto>> Handle(UpdateEmailSMTPSettingCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await emailSMTPSettingRepository.FindAsync(request.Id, cancellationToken);
            if (entityExist == null)
            {
                logger.LogError("Email SMTP setting does not exist.");
                return ServiceResponse<EmailSMTPSettingDto>.Return409("Email SMTP setting does not exist.");
            }
            entityExist.IsDefault = request.IsDefault;
            entityExist.IsEnableSSL = request.IsEnableSSL;
            entityExist.Host = request.Host;
            entityExist.Port = request.Port;
            entityExist.UserName = request.UserName;
            entityExist.Password = request.Password;
            emailSMTPSettingRepository.Update(entityExist);

            // remove other as default
            if (entityExist.IsDefault)
            {
                var defaultEmailSMTPSettings = await emailSMTPSettingRepository.All
                    .Where(c => c.Id != request.Id && c.IsDefault)
                    .ToListAsync(cancellationToken);
                defaultEmailSMTPSettings.ForEach(c => c.IsDefault = false);
                emailSMTPSettingRepository.UpdateRange(defaultEmailSMTPSettings);
            }
            if (await uow.SaveAsync(cancellationToken) <= 0)
            {
                return ServiceResponse<EmailSMTPSettingDto>.Return500();
            }
            var entityDto = mapper.Map<EmailSMTPSettingDto>(entityExist);
            return ServiceResponse<EmailSMTPSettingDto>.ReturnResultWith200(entityDto);
        }
    }
}
