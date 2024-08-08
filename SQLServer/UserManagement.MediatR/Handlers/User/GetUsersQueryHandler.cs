using MediatR;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.MediatR.Queries;
using UserManagement.Repository;

namespace UserManagement.MediatR.Handlers
{
    public class GetUsersQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUsersQuery, UserList>
    {
        public async Task<UserList> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            return await userRepository.GetUsers(request.UserResource);
        }
    }
}
