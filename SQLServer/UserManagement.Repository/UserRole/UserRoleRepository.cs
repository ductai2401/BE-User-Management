using UserManagement.Common.GenericRespository;
using UserManagement.Common.UnitOfWork;
using UserManagement.Data;
using UserManagement.Domain;

namespace UserManagement.Repository
{
    public class UserRoleRepository : GenericRepository<UserRole, UserContext>,
       IUserRoleRepository
    {
        public UserRoleRepository(
            IUnitOfWork<UserContext> uow
            ) : base(uow)
        {
        }
    }
}
