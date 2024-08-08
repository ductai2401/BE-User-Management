using UserManagement.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using UserManagement.Data.Dto;
using System.Threading;

namespace UserManagement.Common.UnitOfWork
{
    public class UnitOfWork<TContext>(
        TContext context,
        ILogger<UnitOfWork<TContext>> logger,
        UserInfoToken userInfoToken) : IUnitOfWork<TContext>
        where TContext : DbContext
    {
        public int Save()
        {
            using var transaction = context.Database.BeginTransaction();
            try
            {
                SetModifiedInformation();
                var retValu = context.SaveChanges();
                transaction.Commit();
                return retValu;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                logger.LogError(e, e.Message);
                return 0;
            }
        }
        public async Task<int> SaveAsync(CancellationToken cancellationToken = default)
        {
            using var transaction = context.Database.BeginTransaction();
            try
            {
                SetModifiedInformation();
                var val = await context.SaveChangesAsync(cancellationToken);
                transaction.Commit();
                return val;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                logger.LogError(e, e.Message);
                return 0;
            }
        }
        public TContext Context => context;
        public void Dispose()
        {
            context.Dispose();
        }

        private void SetModifiedInformation()
        {
            foreach (var entry in Context.ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedDate = DateTime.Now;
                    entry.Entity.CreatedBy = Guid.Parse(userInfoToken.Id);
                    entry.Entity.ModifiedBy = Guid.Parse(userInfoToken.Id);
                    entry.Entity.ModifiedDate = DateTime.Now;
                }
                else if (entry.State == EntityState.Modified)
                {
                    if (entry.Entity.IsDeleted)
                    {
                        entry.Entity.DeletedBy = Guid.Parse(userInfoToken.Id);
                        entry.Entity.DeletedDate = DateTime.Now;
                    }
                    else
                    {
                        entry.Entity.ModifiedBy = Guid.Parse(userInfoToken.Id);
                        entry.Entity.ModifiedDate = DateTime.Now;
                    }
                }
            }
        }
    }
}
