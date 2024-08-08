using AutoMapper;
using UserManagement.Data.Dto;
using UserManagement.MediatR.Queries;
using UserManagement.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace UserManagement.MediatR.Handlers
{
    public class GetAllPageQueryHandler(
        IPageRepository pageRepository,
        IMapper mapper) : IRequestHandler<GetAllPageQuery, List<PageDto>>
    {
        public async Task<List<PageDto>> Handle(GetAllPageQuery request, CancellationToken cancellationToken)
        {
            var entities = await pageRepository.All.ToListAsync(cancellationToken);
            return mapper.Map<List<PageDto>>(entities);
        }
    }
}
