using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UserManagement.Api.Helpers;
using UserManagement.Data.Dto;
using UserManagement.Data.Resources;
using UserManagement.MediatR.Commands;
using UserManagement.MediatR.Queries;
using UserManagement.Repository;

namespace UserManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NLogController(IMediator mediator) : BaseController
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Get System Logs
        /// </summary>
        /// <param name="nLogResource"></param>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json", "application/xml", Type = typeof(NLogList))]
        [ClaimCheck("system_logs_list")]
        public async Task<IActionResult> GetNLogs([FromQuery] NLogResource nLogResource)
        {
            var getAllLoginAuditQuery = new GetNLogsQuery
            {
                NLogResource = nLogResource
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

        /// <summary>
        /// Get Log By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Produces("application/json", "application/xml", Type = typeof(NLogDto))]
        public async Task<IActionResult> GetNLog(Guid id)
        {
            var getLogQuery = new GetLogQuery { Id = id };
            var result = await _mediator.Send(getLogQuery);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Create Log.
        /// </summary>
        /// <param name="addLogCommand"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json", "application/xml", Type = typeof(NLogDto))]
        public async Task<IActionResult> CreatNLog(AddLogCommand addLogCommand)
        {
            var result = await _mediator.Send(addLogCommand);
            return ReturnFormattedResponse(result);
        }
    }
}
