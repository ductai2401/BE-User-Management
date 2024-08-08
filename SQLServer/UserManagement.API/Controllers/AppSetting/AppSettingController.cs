using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using UserManagement.Api.Helpers;
using UserManagement.Data.Dto;
using UserManagement.MediatR.Commands;
using UserManagement.MediatR.Queries;

namespace UserManagement.API.Controllers.AppSetting
{
    /// <summary>
    /// App Setting
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AppSettingController : BaseController
    {
        public IMediator _mediator { get; set; }
        private readonly ILogger<AppSettingController> _logger;
        /// <summary>
        /// App Setting
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="logger"></param>
        public AppSettingController(
            IMediator mediator,
            ILogger<AppSettingController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        /// <summary>
        /// Create  Appsetting
        /// </summary>
        /// <param name="addAppSettingCommand"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json", "application/xml", Type = typeof(AppSettingDto))]
        [ClaimCheck("app_settings_add")]
        public async Task<IActionResult> AddAppSetting(AddAppSettingCommand addAppSettingCommand)
        {
            var result = await _mediator.Send(addAppSettingCommand);
            return ReturnFormattedResponse(result);
        }
        /// <summary>
        /// Update Exist AppSetting By Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateAppSettingCommand"></param>
        /// <returns></returns>

        [HttpPut("{id}")]
        [Produces("application/json", "application/xml", Type = typeof(AppSettingDto))]
        [ClaimCheck("app_settings_edit")]
        public async Task<IActionResult> UpdateAppSetting(Guid id, UpdateAppSettingCommand updateAppSettingCommand)
        {
            updateAppSettingCommand.Id = id;
            var result = await _mediator.Send(updateAppSettingCommand);
            return ReturnFormattedResponse(result);
        }
        /// <summary>
        /// Get AppSetting By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("{id}", Name = "GetAppSetting")]
        [Produces("application/json", "application/xml", Type = typeof(AppSettingDto))]
        [ClaimCheck("app_settings_list")]
        public async Task<IActionResult> GetAppSetting(Guid id)
        {
            _logger.LogTrace("GetAppSetting");
            var getAppSettingQuery = new GetAppSettingQuery
            {
                Id = id
            };

            var result = await _mediator.Send(getAppSettingQuery);
            return ReturnFormattedResponse(result);

        }
        /// <summary>
        /// Get AppSetting By Key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>

        [HttpGet("key/{id}", Name = "GetAppSettingByKey")]
        [Produces("application/json", "application/xml", Type = typeof(AppSettingDto))]
        [ClaimCheck("app_settings_list")]
        public async Task<IActionResult> GetAppSettingByKey(string key)
        {
            _logger.LogTrace("GetAppSettingByKey");
            var getAppSettingByKeyQuery = new GetAppSettingByKeyQuery
            {
                Key = key
            };

            var result = await _mediator.Send(getAppSettingByKeyQuery);
            return ReturnFormattedResponse(result);
        }
        /// <summary>
        /// Get All AppSettings
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetAppSettings")]
        [Produces("application/json", "application/xml", Type = typeof(List<AppSettingDto>))]
        [ClaimCheck("app_settings_list")]
        public async Task<IActionResult> GetAppSettings()
        {
            var getAllAppSettingQuery = new GetAllAppSettingQuery
            {
            };
            var result = await _mediator.Send(getAllAppSettingQuery);
            return ReturnFormattedResponse(result);
        }
        /// <summary>
        /// Delete AppSetting By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        [ClaimCheck("app_settings_add")]
        public async Task<IActionResult> DeleteAppSetting(Guid Id)
        {
            var deleteAppSettingCommand = new DeleteAppSettingCommand
            {
                Id = Id
            };
            var result = await _mediator.Send(deleteAppSettingCommand);
            return ReturnFormattedResponse(result);
        }
    }
}
