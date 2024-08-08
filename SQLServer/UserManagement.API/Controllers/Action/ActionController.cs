using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Data.Dto;
using UserManagement.MediatR.Commands;
using UserManagement.MediatR.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Api.Helpers;

namespace UserManagement.API.Controllers
{
    /// <summary>
    /// Action
    /// </summary>
    [Route("api")]
    [ApiController]
    [Authorize]
    public class ActionController : BaseController
    {
        public IMediator _mediator { get; set; }

        /// <summary>
        /// Action
        /// </summary>
        /// <param name="mediator"></param>
        public ActionController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Get Action By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("Action/{id}", Name = "GetAction")]
        [Produces("application/json", "application/xml", Type = typeof(ActionDto))]
        [ClaimCheck("action_list")]
        public async Task<IActionResult> GetAction(Guid id)
        {
            var getActionQuery = new GetActionQuery { Id = id };
            var result = await _mediator.Send(getActionQuery);
            return ReturnFormattedResponse(result);
        }
        /// <summary>
        /// Get All Actions
        /// </summary>
        /// <returns></returns>
        [HttpGet("Actions")]
        [ClaimCheck("action_list", "role_add", "role_edit", "page_action_edit", "user_permission_edit")]
        [Produces("application/json", "application/xml", Type = typeof(List<ActionDto>))]
        public async Task<IActionResult> GetActions()
        {
            var getAllActionQuery = new GetAllActionQuery { };
            var result = await _mediator.Send(getAllActionQuery);
            return ReturnFormattedResponse(result);
        }
        /// <summary>
        /// Create A Action
        /// </summary>
        /// <param name="addActionCommand"></param>
        /// <returns></returns>
        [HttpPost("Action")]
        [ClaimCheck("action_add")]
        [Produces("application/json", "application/xml", Type = typeof(ActionDto))]
        public async Task<IActionResult> AddAction(AddActionCommand addActionCommand)
        {
            var response = await _mediator.Send(addActionCommand);
            if (!response.Success)
            {
                return ReturnFormattedResponse(response);
            }
            return CreatedAtAction("GetAction", new { id = response.Data.Id }, response.Data);
        }
        /// <summary>
        /// Update Exist Action By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="updateActionCommand"></param>
        /// <returns></returns>
        [HttpPut("Action/{Id}")]
        [ClaimCheck("action_edit")]
        [Produces("application/json", "application/xml", Type = typeof(ActionDto))]
        public async Task<IActionResult> UpdateAction(Guid Id, UpdateActionCommand updateActionCommand)
        {
            updateActionCommand.Id = Id;
            var result = await _mediator.Send(updateActionCommand);
            return ReturnFormattedResponse(result);

        }
        /// <summary>
        /// Delete Action By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("Action/{Id}")]
        [ClaimCheck("action_delete")]
        public async Task<IActionResult> DeleteAction(Guid Id)
        {
            var deleteActionCommand = new DeleteActionCommand { Id = Id };
            var result = await _mediator.Send(deleteActionCommand);
            return ReturnFormattedResponse(result);
        }
    }
}
