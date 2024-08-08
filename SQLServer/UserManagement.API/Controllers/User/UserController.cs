﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Data.Dto;
using UserManagement.MediatR.Commands;
using UserManagement.MediatR.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using UserManagement.Data.Resources;
using UserManagement.Repository;
using UserManagement.Api.Helpers;
using Microsoft.AspNetCore.Http;

namespace UserManagement.API.Controllers
{
    /// <summary>
    /// User
    /// </summary>
    /// <remarks>
    /// User
    /// </remarks>
    /// <param name="mediator"></param>
    /// <param name="userInfo"></param>
    /// <param name="webHostEnvironment"></param>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController(
        IMediator mediator,
        UserInfoToken userInfo,
        IWebHostEnvironment webHostEnvironment
            ) : BaseController
    {
        private readonly IMediator _mediator = mediator;
        private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;
        private readonly UserInfoToken _userInfo = userInfo;

        /// <summary>
        ///  Create a User
        /// </summary>
        /// <param name="addUserCommand"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json", "application/xml", Type = typeof(UserDto))]
        [ClaimCheck("user_add")]
        public async Task<IActionResult> AddUser(AddUserCommand addUserCommand)
        {
            var result = await _mediator.Send(addUserCommand);
            if (!result.Success)
            {
                return ReturnFormattedResponse(result);
            }
            return CreatedAtAction("GetUser", new { id = result.Data.Id }, result.Data);
        }


        /// <summary>
        /// Get All Users
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllUsers")]
        [Produces("application/json", "application/xml", Type = typeof(List<UserDto>))]
        [ClaimCheck("user_list", "user_role_edit")]
        public async Task<IActionResult> GetAllUsers()
        {
            var getAllUserQuery = new GetAllUserQuery { };
            var result = await _mediator.Send(getAllUserQuery);
            return Ok(result);
        }

        /// <summary>
        /// Get User By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetUser")]
        [Produces("application/json", "application/xml", Type = typeof(UserDto))]
        [ClaimCheck("user_edit", "user_permission_edit")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var getUserQuery = new GetUserQuery { Id = id };
            var result = await _mediator.Send(getUserQuery);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Get Users
        /// </summary>
        /// <param name="userResource"></param>
        /// <returns></returns>
        [HttpGet("GetUsers")]
        [Produces("application/json", "application/xml", Type = typeof(UserList))]
        [ClaimCheck("user_list")]
        public async Task<IActionResult> GetUsers([FromQuery] UserResource userResource)
        {
            var getAllLoginAuditQuery = new GetUsersQuery
            {
                UserResource = userResource
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
        /// Get Recently Registered Users
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetRecentlyRegisteredUsers")]
        [Produces("application/json", "application/xml", Type = typeof(List<UserDto>))]
        [ClaimCheck("dashboard_list")]
        public async Task<IActionResult> GetRecentlyRegisteredUsers()
        {
            var getRecentlyRegisteredUserQuery = new GetRecentlyRegisteredUserQuery { };
            var result = await _mediator.Send(getRecentlyRegisteredUserQuery);
            return Ok(result);
        }


        /// <summary>
        /// User Login
        /// </summary>
        /// <param name="userLoginCommand"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [AllowAnonymous]
        [Produces("application/json", "application/xml", Type = typeof(UserAuthDto))]
        public async Task<IActionResult> UserLogin(UserLoginCommand userLoginCommand)
        {
            userLoginCommand.RemoteIp = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            var result = await _mediator.Send(userLoginCommand);

            if (!result.Success)
            {
                return ReturnFormattedResponse(result);
            }
            return Ok(new
            {
                result.Data.BearerToken
            });
        }

        /// <summary>
        /// Update User By Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateUserCommand"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json", "application/xml", Type = typeof(UserDto))]
        [ClaimCheck("user_edit")]
        public async Task<IActionResult> UpdateUser(Guid id, UpdateUserCommand updateUserCommand)
        {
            updateUserCommand.Id = id;
            var result = await _mediator.Send(updateUserCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Update Profile
        /// </summary>
        /// <param name="updateUserProfileCommand"></param>
        /// <returns></returns>
        [HttpPut("profile")]
        [Produces("application/json", "application/xml", Type = typeof(UserDto))]
        public async Task<IActionResult> UpdateUserProfile(UpdateUserProfileCommand updateUserProfileCommand)
        {
            var result = await _mediator.Send(updateUserProfileCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Update Profile photo
        /// </summary>
        /// <returns></returns>
        [HttpPost("UpdateUserProfilePhoto"), DisableRequestSizeLimit]
        [Produces("application/json", "application/xml", Type = typeof(UserDto))]
        public async Task<IActionResult> UpdateUserProfilePhoto()
        {
            var updateUserProfilePhotoCommand = new UpdateUserProfilePhotoCommand()
            {
                FormFile = Request.Form.Files,
                RootPath = _webHostEnvironment.WebRootPath
            };
            var result = await _mediator.Send(updateUserProfilePhotoCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Delete User By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        [ClaimCheck("user_delete")]
        public async Task<IActionResult> DeleteUser(Guid Id)
        {
            var deleteUserCommand = new DeleteUserCommand { Id = Id };
            var result = await _mediator.Send(deleteUserCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// User Change Password
        /// </summary>
        /// <param name="resetPasswordCommand"></param>
        /// <returns></returns>
        [HttpPost("changepassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordCommand resetPasswordCommand)
        {
            var result = await _mediator.Send(resetPasswordCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Reset Resetpassword
        /// </summary>
        /// <param name="newPasswordCommand"></param>
        /// <returns></returns>
        [HttpPost("resetpassword")]
        [ClaimCheck("rest_password_edit")]
        public async Task<IActionResult> ResetPassword(ResetPasswordCommand newPasswordCommand)
        {
            var result = await _mediator.Send(newPasswordCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Get User Profile
        /// </summary>
        /// <returns></returns>
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var getUserQuery = new GetUserQuery
            {
                Id = Guid.Parse(_userInfo.Id)
            };
            var result = await _mediator.Send(getUserQuery);
            if (!string.IsNullOrWhiteSpace(result.Data.ProfilePhoto))
            {
                result.Data.ProfilePhoto = $"Users/{result.Data.ProfilePhoto}";
            }
            return ReturnFormattedResponse(result);
        }

    }
}
