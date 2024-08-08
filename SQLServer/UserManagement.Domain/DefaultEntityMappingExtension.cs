using UserManagement.Data;
using Microsoft.EntityFrameworkCore;

namespace UserManagement.Domain
{
    public static class DefaultEntityMappingExtension
    {
        public static void DefalutMappingValue(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Action>()
               .Property(b => b.ModifiedDate)
               .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Page>()
                .Property(b => b.ModifiedDate)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<PageAction>()
                .Property(b => b.ModifiedDate)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<User>()
                .Property(b => b.ModifiedDate)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Role>()
                .Property(b => b.ModifiedDate)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<AppSetting>()
              .Property(b => b.ModifiedDate)
              .HasDefaultValueSql("getdate()");

        }

        public static void DefalutDeleteValueFilter(this ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>()
            .HasQueryFilter(p => !p.IsDeleted);

            modelBuilder.Entity<Role>()
            .HasQueryFilter(p => !p.IsDeleted);

            modelBuilder.Entity<Action>()
              .HasQueryFilter(p => !p.IsDeleted);

            modelBuilder.Entity<Page>()
             .HasQueryFilter(p => !p.IsDeleted);

            modelBuilder.Entity<PageAction>()
             .HasQueryFilter(p => !p.IsDeleted);

            modelBuilder.Entity<AppSetting>()
             .HasQueryFilter(p => !p.IsDeleted);

            modelBuilder.Entity<EmailTemplate>()
          .HasQueryFilter(p => !p.IsDeleted);

            modelBuilder.Entity<EmailSMTPSetting>()
            .HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
