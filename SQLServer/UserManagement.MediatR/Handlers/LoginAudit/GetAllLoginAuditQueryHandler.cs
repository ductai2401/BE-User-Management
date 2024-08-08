using MediatR;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.MediatR.Queries;
using UserManagement.Repository;

namespace UserManagement.MediatR.Handlers
{
    public class GetAllLoginAuditQueryHandler(ILoginAuditRepository loginAuditRepository) : IRequestHandler<GetAllLoginAuditQuery, LoginAuditList>
    {
        public async Task<LoginAuditList> Handle(GetAllLoginAuditQuery request, CancellationToken cancellationToken)
        {
            return await loginAuditRepository.GetDocumentAuditTrails(request.LoginAuditResource);
        }
    }
}
