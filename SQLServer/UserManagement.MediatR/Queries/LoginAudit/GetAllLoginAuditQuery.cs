using MediatR;
using UserManagement.Data.Resources;
using UserManagement.Repository;

namespace UserManagement.MediatR.Queries
{
    public class GetAllLoginAuditQuery : IRequest<LoginAuditList>
    {
        public LoginAuditResource LoginAuditResource { get; set; }
    }
}
