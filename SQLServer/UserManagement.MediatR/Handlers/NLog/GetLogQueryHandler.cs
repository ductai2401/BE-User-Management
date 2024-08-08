using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Data.Dto;
using UserManagement.Helper;
using UserManagement.MediatR.Queries;
using UserManagement.Repository;

namespace UserManagement.MediatR.Handlers
{
    class GetLogQueryHandler(
       INLogRespository nLogRespository,
       IMapper mapper) : IRequestHandler<GetLogQuery, ServiceResponse<NLogDto>>
    {
        public async Task<ServiceResponse<NLogDto>> Handle(GetLogQuery request, CancellationToken cancellationToken)
        {
            var entity = await nLogRespository.FindAsync(request.Id, cancellationToken);
            if (entity != null)
                return ServiceResponse<NLogDto>.ReturnResultWith200(mapper.Map<NLogDto>(entity));
            else
                return ServiceResponse<NLogDto>.Return404();
        }
    }
}
