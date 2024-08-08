using UserManagement.Common.GenericRespository;
using UserManagement.Common.UnitOfWork;
using UserManagement.Data;
using UserManagement.Domain;

namespace UserManagement.Repository
{
    public class UserAllowedIPRepository : GenericRepository<UserAllowedIP, UserContext>,
        IUserAllowedIPRepository
    {
        public UserAllowedIPRepository(
            IUnitOfWork<UserContext> uow
            ) : base(uow)
        {
        }
    }
}
