using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Common.GenericRespository;
using UserManagement.Common.UnitOfWork;
using UserManagement.Data;
using UserManagement.Data.Dto;
using UserManagement.Data.Resources;
using UserManagement.Domain;

namespace UserManagement.Repository
{
    public class LoginAuditRepository(
        IUnitOfWork<UserContext> uow,
        ILogger<LoginAuditRepository> logger,
        IPropertyMappingService propertyMappingService) : GenericRepository<LoginAudit,
            UserContext>(uow), ILoginAuditRepository
    {
        private new readonly IUnitOfWork<UserContext> _uow = uow;
        private readonly ILogger<LoginAuditRepository> _logger = logger;
        private readonly IPropertyMappingService _propertyMappingService = propertyMappingService;

        public async Task<LoginAuditList> GetDocumentAuditTrails(LoginAuditResource loginAuditResrouce)
        {
            var collectionBeforePaging = All;
            collectionBeforePaging =
               collectionBeforePaging.ApplySort(loginAuditResrouce.OrderBy,
               _propertyMappingService.GetPropertyMapping<LoginAuditDto, LoginAudit>());

            if (!string.IsNullOrWhiteSpace(loginAuditResrouce.UserName))
            {
                collectionBeforePaging = collectionBeforePaging
                    .Where(c => EF.Functions.Like(c.UserName, $"%{loginAuditResrouce.UserName}%"));
            }

            var loginAudits = new LoginAuditList();
            return await loginAudits.Create(
                collectionBeforePaging,
                loginAuditResrouce.Skip,
                loginAuditResrouce.PageSize
                );
        }

        public async Task LoginAudit(LoginAuditDto loginAudit)
        {
            try
            {
                Add(new LoginAudit
                {
                    Id = Guid.NewGuid(),
                    LoginTime = DateTime.Now.ToLocalTime(),
                    Provider = loginAudit.Provider,
                    Status = loginAudit.Status,
                    UserName = loginAudit.UserName,
                    RemoteIP = loginAudit.RemoteIP,
                    Latitude = loginAudit.Latitude,
                    Longitude = loginAudit.Longitude
                });
                await _uow.SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("{Message}", ex.Message);
            }
        }
    }
}
