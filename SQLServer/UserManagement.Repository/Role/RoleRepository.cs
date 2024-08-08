using UserManagement.Common.GenericRespository;
using UserManagement.Common.UnitOfWork;
using UserManagement.Data;
using UserManagement.Domain;

namespace UserManagement.Repository
{
    public  class RoleRepository : GenericRepository<Role, UserContext>,
          IRoleRepository
    {
        public RoleRepository(
            IUnitOfWork<UserContext> uow
            ) : base(uow)
        {
        }
    }
}