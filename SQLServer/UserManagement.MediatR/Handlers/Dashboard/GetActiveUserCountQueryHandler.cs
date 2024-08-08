using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.MediatR.Queries;
using UserManagement.Repository;

namespace UserManagement.MediatR.Handlers
{
    public class GetActiveUserCountQueryHandler(IUserRepository userRepository) : IRequestHandler<GetActiveUserCountQuery, int>
    {
        public async Task<int> Handle(GetActiveUserCountQuery request, CancellationToken cancellationToken)
        {
            return await userRepository.All.Where(c => c.IsActive).CountAsync(cancellationToken);
        }
    }
}
