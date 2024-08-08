using UserManagement.Common.GenericRespository;
using UserManagement.Common.UnitOfWork;
using UserManagement.Data;
using UserManagement.Domain;

namespace UserManagement.Repository
{
    public class EmailTemplateRepository : GenericRepository<EmailTemplate, UserContext>,
          IEmailTemplateRepository
    {
        public EmailTemplateRepository(
            IUnitOfWork<UserContext> uow
            ) : base(uow)
        {

        }
    }
}

