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
    public class UpdateEmailTemplateCommandHandler(
        IEmailTemplateRepository emailTemplateRepository,
        IMapper mapper,
        IUnitOfWork<UserContext> uow,
        UserInfoToken userInfoToken,
        ILogger<UpdateEmailTemplateCommandHandler> logger
        ) : IRequestHandler<UpdateEmailTemplateCommand, ServiceResponse<EmailTemplateDto>>
    {
        public async Task<ServiceResponse<EmailTemplateDto>> Handle(UpdateEmailTemplateCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await emailTemplateRepository
                .FindBy(c => c.Name == request.Name && c.Id != request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (entityExist != null)
            {
                logger.LogError("Email Template already exist.");
                return ServiceResponse<EmailTemplateDto>.Return409("Email Template already exist.");
            }

            var entity = mapper.Map<EmailTemplate>(request);
            entity.ModifiedBy = Guid.Parse(userInfoToken.Id);
            emailTemplateRepository.Update(entity);

            if (await uow.SaveAsync(cancellationToken) <= 0)
            {
                return ServiceResponse<EmailTemplateDto>.Return500();
            }
            var entityDto = mapper.Map<EmailTemplateDto>(entity);
            return ServiceResponse<EmailTemplateDto>.ReturnResultWith200(entityDto);
        }
    }
}
