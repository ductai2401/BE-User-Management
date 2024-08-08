using AutoMapper;
using UserManagement.Data.Dto;
using UserManagement.MediatR.Queries;
using UserManagement.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace UserManagement.MediatR.Handlers
{
    public class GetAllPageActionQueryHandler(
        IPageActionRepository pageActionRepository,
        IMapper mapper
        ) : IRequestHandler<GetAllPageActionQuery, List<PageActionDto>>
    {
        public async Task<List<PageActionDto>> Handle(GetAllPageActionQuery request, CancellationToken cancellationToken)
        {
            var entities = await pageActionRepository.All.ToListAsync(cancellationToken);
            return mapper.Map<List<PageActionDto>>(entities);
        }
    }
}
