using UserManagement.Common.GenericRespository;
using UserManagement.Common.UnitOfWork;
using UserManagement.Data;
using UserManagement.Domain;

namespace UserManagement.Repository
{
    public class AppSettingRepository : GenericRepository<AppSetting, UserContext>,
          IAppSettingRepository
    {
        public AppSettingRepository(
            IUnitOfWork<UserContext> uow
            ) : base(uow)
        {

        }
    }
}
