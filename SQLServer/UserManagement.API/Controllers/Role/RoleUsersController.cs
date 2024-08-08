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
    /// RoleUsers
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoleUsersController : BaseController
    {
        public IMediator _mediator { get; set; }
        /// <summary>
        /// RoleUsers
        /// </summary>
        /// <param name="mediator"></param>
        public RoleUsersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Get Role Users By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "RoleUsers")]
        [Produces("application/json", "application/xml", Type = typeof(List<UserRoleDto>))]
        [ClaimCheck("user_role_edit")]
        public async Task<IActionResult> RoleUsers(Guid id)
        {
            var getUserQuery = new GetRoleUsersQuery { RoleId = id };
            var result = await _mediator.Send(getUserQuery);
            return Ok(result);
        }
        /// <summary>
        /// Update Role Users By Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateRoleCommand"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json", "application/xml", Type = typeof(UserRoleDto))]
        [ClaimCheck("user_role_edit")]
        public async Task<IActionResult> UpdateRoleUsers(Guid id, UpdateUserRoleCommand updateRoleCommand)
        {
            updateRoleCommand.Id = id;
            var result = await _mediator.Send(updateRoleCommand);
            return ReturnFormattedResponse(result);
        }
    }
}
