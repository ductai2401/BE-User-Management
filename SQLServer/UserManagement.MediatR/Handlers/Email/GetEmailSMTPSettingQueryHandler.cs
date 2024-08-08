using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Data.Dto;
using UserManagement.Helper;
using UserManagement.MediatR.Queries;
using UserManagement.Repository;

namespace UserManagement.MediatR.Handlers
{
    public class GetEmailSMTPSettingQueryHandler(
        IEmailSMTPSettingRepository emailSMTPSettingRepository,
        IMapper mapper,
        ILogger<GetRoleQueryHandler> logger
        ) : IRequestHandler<GetEmailSMTPSettingQuery, ServiceResponse<EmailSMTPSettingDto>>
    {
        public async Task<ServiceResponse<EmailSMTPSettingDto>> Handle(GetEmailSMTPSettingQuery request, CancellationToken cancellationToken)
        {
            var entity = await emailSMTPSettingRepository.All.Where(c => c.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (entity != null)
                return ServiceResponse<EmailSMTPSettingDto>.ReturnResultWith200(mapper.Map<EmailSMTPSettingDto>(entity));
            else
            {
                logger.LogError("Not found");
                return ServiceResponse<EmailSMTPSettingDto>.Return404();
            }
        }
    }
}
