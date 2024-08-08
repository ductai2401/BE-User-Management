using AutoMapper;
using UserManagement.Data.Dto;
using UserManagement.MediatR.Queries;
using UserManagement.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Helper;

namespace UserManagement.MediatR.Handlers
{
    public class GetPageActionQueryHandler(
        IPageActionRepository pageActionRepository,
        IMapper mapper,
        ILogger<GetPageActionQueryHandler> logger
        ) : IRequestHandler<GetPageActionQuery, ServiceResponse<PageActionDto>>
    {
        public async Task<ServiceResponse<PageActionDto>> Handle(GetPageActionQuery request, CancellationToken cancellationToken)
        {
            var entity = await pageActionRepository.FindAsync(request.Id, cancellationToken);
            if (entity != null)
                return ServiceResponse<PageActionDto>.ReturnResultWith200(mapper.Map<PageActionDto>(entity));
            else
            {
                logger.LogWarning("Role Not Found");
                return ServiceResponse<PageActionDto>.Return404();
            }
        }
    }
}
