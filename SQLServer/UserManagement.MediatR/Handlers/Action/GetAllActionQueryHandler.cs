using AutoMapper;
using UserManagement.Data.Dto;
using UserManagement.MediatR.Queries;
using UserManagement.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Helper;

namespace UserManagement.MediatR.Handlers
{
    public class GetAllActionQueryHandler(
        IActionRepository actionRepository,
        IMapper mapper) : IRequestHandler<GetAllActionQuery, ServiceResponse<List<ActionDto>>>
    {
        public async Task<ServiceResponse<List<ActionDto>>> Handle(GetAllActionQuery request, CancellationToken cancellationToken)
        {
            var entities = await actionRepository.All.ToListAsync(cancellationToken);
            return ServiceResponse<List<ActionDto>>.ReturnResultWith200(mapper.Map<List<ActionDto>>(entities));
        }
    }
}
