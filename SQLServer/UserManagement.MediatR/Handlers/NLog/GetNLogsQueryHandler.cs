using MediatR;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.MediatR.Queries;
using UserManagement.Repository;

namespace UserManagement.MediatR.Handlers
{
    public class GetNLogsQueryHandler(INLogRespository nLogRespository) : IRequestHandler<GetNLogsQuery, NLogList>
    {
        public async Task<NLogList> Handle(GetNLogsQuery request, CancellationToken cancellationToken)
        {
            return await nLogRespository.GetNLogsAsync(request.NLogResource);
        }
    }
}
