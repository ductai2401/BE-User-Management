using MediatR;

namespace UserManagement.MediatR.Queries
{
    public class GetActiveUserCountQuery : IRequest<int>
    {
    }
}
