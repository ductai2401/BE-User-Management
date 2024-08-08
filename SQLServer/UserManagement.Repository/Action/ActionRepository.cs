using UserManagement.Common.GenericRespository;
using UserManagement.Common.UnitOfWork;
using UserManagement.Data;
using UserManagement.Domain;

namespace UserManagement.Repository
{
    public class ActionRepository : GenericRepository<Action, UserContext>,
          IActionRepository
    {
        public ActionRepository(
            IUnitOfWork<UserContext> uow
            ) : base(uow)
        {
        }
    }
}
