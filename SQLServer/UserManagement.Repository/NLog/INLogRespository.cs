using System.Threading.Tasks;
using UserManagement.Common.GenericRespository;
using UserManagement.Data;
using UserManagement.Data.Resources;

namespace UserManagement.Repository
{
    public interface INLogRespository : IGenericRepository<NLog>
    {
        Task<NLogList> GetNLogsAsync(NLogResource nLogResource);
    }
}
