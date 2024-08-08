using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.MediatR.Queries;
using UserManagement.Repository;

namespace UserManagement.MediatR.Handlers
{
    public class GetTotalUserCountQueryHandler(IUserRepository userRepository)
        : IRequestHandler<GetTotalUserCountQuery, int>
    {
        public async Task<int> Handle(GetTotalUserCountQuery request, CancellationToken cancellationToken)
        {
            return await userRepository.All.CountAsync(cancellationToken);
        }
    }
}
