using MediatR;
using UserManagement.Data.Resources;
using UserManagement.Repository;

namespace UserManagement.MediatR.Queries
{
    public class GetNLogsQuery : IRequest<NLogList>
    {
        public NLogResource NLogResource { get; set; }
    }
}
