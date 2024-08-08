using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Api.Helpers;
using UserManagement.Data.Dto;
using UserManagement.MediatR.Commands;
using UserManagement.MediatR.Queries;

namespace UserManagement.API.Controllers.Email
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailSMTPSettingController : BaseController
    {
        IMediator _mediator;
        public EmailSMTPSettingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create an Email SMTP Configuration.
        /// </summary>
        /// <param name="addEmailSMTPSettingCommand"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json", "application/xml", Type = typeof(EmailSMTPSettingDto))]
        [ClaimCheck("email_smtp_add")]
        public async Task<IActionResult> AddEmailSMTPSetting(AddEmailSMTPSettingCommand addEmailSMTPSettingCommand)
        {
            var result = await _mediator.Send(addEmailSMTPSettingCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Get Email SMTP Configuration.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Produces("application/json", "application/xml", Type = typeof(EmailSMTPSettingDto))]
        [ClaimCheck("email_smtp_edit")]
        public async Task<IActionResult> GetEmailSMTPSetting(Guid id)
        {
            var query = new GetEmailSMTPSettingQuery() { Id = id };
            var result = await _mediator.Send(query);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Get Email SMTP Configuration list.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json", "application/xml", Type = typeof(List<EmailSMTPSettingDto>))]
        [ClaimCheck("email_smtp_list")]
        public async Task<IActionResult> GetEmailSMTPSettings()
        {
            var query = new GetEmailSMTPSettingsQuery() { };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Update an Email SMTP Configuration.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateEmailSMTPSettingCommand"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json", "application/xml", Type = typeof(EmailSMTPSettingDto))]
        [ClaimCheck("email_smtp_edit")]
        public async Task<IActionResult> UpdateEmailSMTPSetting(Guid id, UpdateEmailSMTPSettingCommand updateEmailSMTPSettingCommand)
        {
            updateEmailSMTPSettingCommand.Id = id;
            var result = await _mediator.Send(updateEmailSMTPSettingCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Delete an Email SMTP Configuration.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Produces("application/json", "application/xml", Type = typeof(EmailSMTPSettingDto))]
        [ClaimCheck("email_smtp_delete")]
        public async Task<IActionResult> DeleteEmailSMTPSetting(Guid id)
        {
            var deleteEmailSMTPSettingCommand = new DeleteEmailSMTPSettingCommand() { Id = id };
            var result = await _mediator.Send(deleteEmailSMTPSettingCommand);
            return ReturnFormattedResponse(result);
        }
    }
}
