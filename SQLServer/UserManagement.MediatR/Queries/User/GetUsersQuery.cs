using MediatR;
using UserManagement.Data.Resources;
using UserManagement.Repository;

namespace UserManagement.MediatR.Queries
{
    public class GetUsersQuery : IRequest<UserList>
    {
        public UserResource UserResource { get; set; }
    }
}
