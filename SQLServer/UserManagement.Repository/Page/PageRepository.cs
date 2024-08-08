using UserManagement.Common.GenericRespository;
using UserManagement.Common.UnitOfWork;
using UserManagement.Data;
using UserManagement.Domain;

namespace UserManagement.Repository
{
    public class PageRepository : GenericRepository<Page, UserContext>,
          IPageRepository
    {
        public PageRepository(
            IUnitOfWork<UserContext> uow
            ) : base(uow)
        {
        }
    }
}
