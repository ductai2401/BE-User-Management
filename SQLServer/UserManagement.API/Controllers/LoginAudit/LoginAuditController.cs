using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserManagement.Api.Helpers;
using UserManagement.Data.Resources;
using UserManagement.MediatR.Queries;
using UserManagement.Repository;

namespace UserManagement.API.Controllers.LoginAudit
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LoginAuditController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Get All Login Audit detail
        /// </summary>
        /// <param name="loginAuditResource"></param>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json", "application/xml", Type = typeof(LoginAuditList))]
        [ClaimCheck("login_audit_list")]
        public async Task<IActionResult> GetLoginAudit([FromQuery] LoginAuditResource loginAuditResource)
        {
            var getAllLoginAuditQuery = new GetAllLoginAuditQuery
            {
                LoginAuditResource = loginAuditResource
            };
            var result = await _mediator.Send(getAllLoginAuditQuery);

            var paginationMetadata = new
            {
                totalCount = result.TotalCount,
                pageSize = result.PageSize,
                skip = result.Skip,
                totalPages = result.TotalPages
            };
            Response.Headers.Append("X-Pagination",
                Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));
            return Ok(result);
        }
    }
}
