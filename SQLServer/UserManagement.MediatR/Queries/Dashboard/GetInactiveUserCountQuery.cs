using MediatR;

namespace UserManagement.MediatR.Queries
{
    public class GetInactiveUserCountQuery : IRequest<int>
    {
    }
}
