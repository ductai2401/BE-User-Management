using AutoMapper;
using UserManagement.Data.Dto;
using UserManagement.MediatR.Queries;
using UserManagement.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Helper;
using Microsoft.Extensions.Logging;

namespace UserManagement.MediatR.Handlers
{
    public class GetPageQueryHandler(
        IPageRepository pageRepository,
        IMapper mapper,
        ILogger<GetPageQueryHandler> logger) : IRequestHandler<GetPageQuery, ServiceResponse<PageDto>>
    {
        public async Task<ServiceResponse<PageDto>> Handle(GetPageQuery request, CancellationToken cancellationToken)
        {
            var entity = await pageRepository.FindAsync(request.Id, cancellationToken);
            if (entity != null)
                return ServiceResponse<PageDto>.ReturnResultWith200(mapper.Map<PageDto>(entity));
            else
            {
                logger.LogError("Not found");
                return ServiceResponse<PageDto>.Return404();
            }
        }
    }
}
