using AutoMapper;
using UserManagement.Data.Dto;
using UserManagement.MediatR.Queries;
using UserManagement.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Helper;

namespace UserManagement.MediatR.Handlers
{
    public class GetActionQueryHandler(
       IActionRepository actionRepository,
       IMapper mapper) : IRequestHandler<GetActionQuery, ServiceResponse<ActionDto>>
    {
        public async Task<ServiceResponse<ActionDto>> Handle(GetActionQuery request, CancellationToken cancellationToken)
        {
            var entity = await actionRepository.FindAsync(request.Id, cancellationToken);
            if (entity != null)
                return ServiceResponse<ActionDto>.ReturnResultWith200(mapper.Map<ActionDto>(entity));
            else
                return ServiceResponse<ActionDto>.Return404();
        }
    }
}
