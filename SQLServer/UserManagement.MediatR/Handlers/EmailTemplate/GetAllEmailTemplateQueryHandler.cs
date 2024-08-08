using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Data.Dto;
using UserManagement.Helper;
using UserManagement.MediatR.Queries;
using UserManagement.Repository;

namespace UserManagement.MediatR.Handlers
{
    public class GetAllEmailTemplateQueryHandler(
        IEmailTemplateRepository emailTemplateRepository,
        IMapper mapper
        ) : IRequestHandler<GetAllEmailTemplateQuery, ServiceResponse<List<EmailTemplateDto>>>
    {
        public async Task<ServiceResponse<List<EmailTemplateDto>>> Handle(GetAllEmailTemplateQuery request, CancellationToken cancellationToken)
        {
            var entities = await emailTemplateRepository.All.ToListAsync(cancellationToken);
            return ServiceResponse<List<EmailTemplateDto>>.ReturnResultWith200(mapper.Map<List<EmailTemplateDto>>(entities));
        }
    }
}
