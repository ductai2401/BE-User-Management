using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Data.Dto;
using UserManagement.Helper;
using UserManagement.MediatR.Queries;
using UserManagement.Repository;

namespace UserManagement.MediatR.Handlers
{
    public class GetEmailTemplateQueryHandler(
        IEmailTemplateRepository emailTemplateRepository,
        IMapper mapper,
        ILogger<GetEmailTemplateQueryHandler> logger
        ) : IRequestHandler<GetEmailTemplateQuery, ServiceResponse<EmailTemplateDto>>
    {
        public async Task<ServiceResponse<EmailTemplateDto>> Handle(GetEmailTemplateQuery request, CancellationToken cancellationToken)
        {
            var emailTemplate = await emailTemplateRepository.FindBy(c => c.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (emailTemplate == null)
            {
                logger.LogError("Email Template is not available");
                return ServiceResponse<EmailTemplateDto>.Return404();
            }

            return ServiceResponse<EmailTemplateDto>.ReturnResultWith200(mapper.Map<EmailTemplateDto>(emailTemplate));
        }
    }
}
