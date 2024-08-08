using UserManagement.Common.GenericRespository;
using UserManagement.Common.UnitOfWork;
using UserManagement.Data;
using UserManagement.Domain;

namespace UserManagement.Repository
{
    public class EmailSMTPSettingRepository : GenericRepository<EmailSMTPSetting, UserContext>,
           IEmailSMTPSettingRepository
    {
        public EmailSMTPSettingRepository(
            IUnitOfWork<UserContext> uow
            ) : base(uow)
        {
        }
    }
}
