using MediatR;
using Microsoft.Extensions.Logging;
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
    public class DeleteEmailSMTPSettingCommandHandler(
        IEmailSMTPSettingRepository emailSMTPSettingRepository,
        IUnitOfWork<UserContext> uow,
        ILogger<DeleteEmailSMTPSettingCommandHandler> logger
        ) : IRequestHandler<DeleteEmailSMTPSettingCommand, ServiceResponse<EmailSMTPSettingDto>>
    {
        public async Task<ServiceResponse<EmailSMTPSettingDto>> Handle(DeleteEmailSMTPSettingCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await emailSMTPSettingRepository.FindAsync(request.Id, cancellationToken);
            if (entityExist == null)
            {
                logger.LogError("Not found");
                return ServiceResponse<EmailSMTPSettingDto>.Return404();
            }
            if (entityExist.IsDefault)
            {
                return ServiceResponse<EmailSMTPSettingDto>.Return422("You can not delete default Setting.");
            }
            entityExist.IsDeleted = true;
            emailSMTPSettingRepository.Update(entityExist);
            if (await uow.SaveAsync(cancellationToken) <= 0)
            {
                return ServiceResponse<EmailSMTPSettingDto>.Return500();
            }
            return ServiceResponse<EmailSMTPSettingDto>.ReturnResultWith204();
        }
    }
}
