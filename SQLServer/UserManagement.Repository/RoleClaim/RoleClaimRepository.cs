using UserManagement.Common.GenericRespository;
using UserManagement.Common.UnitOfWork;
using UserManagement.Data;
using UserManagement.Domain;

namespace UserManagement.Repository
{
    public class RoleClaimRepository : GenericRepository<RoleClaim, UserContext>,
           IRoleClaimRepository
    {
        public RoleClaimRepository(
            IUnitOfWork<UserContext> uow
            ) : base(uow)
        {
        }
    }
}