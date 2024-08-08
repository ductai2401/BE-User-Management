using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace UserManagement.Common.UnitOfWork
{
    public interface IUnitOfWork<TContext>
        where TContext : DbContext
    {
        int Save();
        Task<int> SaveAsync(CancellationToken cancellationToken = default);
        TContext Context { get; }
    }
}
