using System.Threading.Tasks;
using UserManagement.Common.GenericRespository;
using UserManagement.Data;
using UserManagement.Data.Dto;
using UserManagement.Data.Resources;

namespace UserManagement.Repository
{
    public interface ILoginAuditRepository : IGenericRepository<LoginAudit>
    {
        Task<LoginAuditList> GetDocumentAuditTrails(LoginAuditResource loginAuditResrouce);
        Task LoginAudit(LoginAuditDto loginAudit);
    }
}
