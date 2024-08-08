using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Data.Dto;
using UserManagement.MediatR.Queries;
using UserManagement.Repository;

namespace UserManagement.MediatR.Handlers
{
    public class GetEmailSMTPSettingsQueryHandler(
        IEmailSMTPSettingRepository emailSMTPSettingRepository,
        IMapper mapper) : IRequestHandler<GetEmailSMTPSettingsQuery, List<EmailSMTPSettingDto>>
    {
        public async Task<List<EmailSMTPSettingDto>> Handle(GetEmailSMTPSettingsQuery request, CancellationToken cancellationToken)
        {
            var entities = await emailSMTPSettingRepository.All.ToListAsync(cancellationToken);
            return mapper.Map<List<EmailSMTPSettingDto>>(entities);
        }
    }
}
