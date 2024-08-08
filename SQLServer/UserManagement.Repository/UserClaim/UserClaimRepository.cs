using UserManagement.Common.GenericRespository;
using UserManagement.Common.UnitOfWork;
using UserManagement.Data;
using UserManagement.Domain;

namespace UserManagement.Repository
{
    public class UserClaimRepository : GenericRepository<UserClaim, UserContext>,
           IUserClaimRepository
    {
        public UserClaimRepository(
            IUnitOfWork<UserContext> uow
            ) : base(uow)
        {
        }
    }
}