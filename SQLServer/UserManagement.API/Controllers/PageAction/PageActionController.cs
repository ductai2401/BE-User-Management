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
    /// Page Action
    /// </summary>
    [Route("api")]
    [ApiController]
    [Authorize]
    public class PageActionController : BaseController
    {
        public IMediator _mediator { get; set; }
        /// <summary>
        /// Page Action
        /// </summary>
        /// <param name="mediator"></param>
        public PageActionController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Get All Page Actions
        /// </summary>
        /// <returns></returns>
        [HttpGet("PageActions")]
        [Produces("application/json", "application/xml", Type = typeof(List<PageActionDto>))]
        [ClaimCheck("page_action_edit", "role_add", "role_edit", "user_permission_edit")]
        public async Task<IActionResult> GetPageActions()
        {
            var getAllPageActionQuery = new GetAllPageActionQuery { };
            var result = await _mediator.Send(getAllPageActionQuery);
            return Ok(result);
        }
        /// <summary>
        /// Add Page Action
        /// </summary>
        /// <param name="addPageActionCommand"></param>
        /// <returns></returns>
        [HttpPost("PageAction")]
        [Produces("application/json", "application/xml", Type = typeof(PageActionDto))]
        [ClaimCheck("page_action_edit")]
        public async Task<IActionResult> AddPageAction(AddPageActionCommand addPageActionCommand)
        {
            var result = await _mediator.Send(addPageActionCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Delete Page Action By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("PageAction/{Id}")]
        [ClaimCheck("page_action_edit")]
        public async Task<IActionResult> DeletePageAction(Guid Id)
        {
            var deletePageActionCommand = new DeletePageActionCommand { Id = Id };
            var result = await _mediator.Send(deletePageActionCommand);
            return ReturnFormattedResponse(result);
        }
    }
}
