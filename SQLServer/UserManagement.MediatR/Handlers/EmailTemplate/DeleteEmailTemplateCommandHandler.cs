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
    public class DeleteEmailTemplateCommandHandler(
        IEmailTemplateRepository emailTemplateRepository,
        IUnitOfWork<UserContext> uow,
        ILogger<UpdateAppSettingCommandHandler> logger
        ) : IRequestHandler<DeleteEmailTemplateCommand, ServiceResponse<bool>>
    {
        public async Task<ServiceResponse<bool>> Handle(DeleteEmailTemplateCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await emailTemplateRepository.FindAsync(request.Id, cancellationToken);
            if (entityExist == null)
            {
                logger.LogError("Email Template Not Found.");
                return ServiceResponse<bool>.Return404();
            }
            entityExist.IsDeleted = true;
            emailTemplateRepository.Update(entityExist);
            if (await uow.SaveAsync(cancellationToken) <= 0)
            {
                return ServiceResponse<bool>.Return500();
            }
            return ServiceResponse<bool>.ReturnResultWith204();
        }
    }
}
