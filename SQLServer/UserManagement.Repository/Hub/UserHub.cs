using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Data.Dto;
using UserManagement.Data.Dto.User;

namespace UserManagement.Repository
{
    public class UserHub(IConnectionMappingRepository userInfoInMemory) : Hub<IHubClient>
    {
        private readonly IConnectionMappingRepository _userInfoInMemory = userInfoInMemory;

        public async Task Leave(string id)
        {
            var userInfo = _userInfoInMemory.GetUserInfoByName(id);
            _userInfoInMemory.Remove(userInfo);
            await Clients.AllExcept([Context.ConnectionId])
                .UserLeft(id);
        }
        public async Task Logout(string id)
        {
            var userInfo = _userInfoInMemory.GetUserInfoByName(id);
            if (userInfo != null)
            {
                _userInfoInMemory.Remove(userInfo);
                await Clients.AllExcept([Context.ConnectionId])
                    .UserLeft(id);
            }
        }
        public async Task ForceLogout(string id)
        {
            var userInfo = _userInfoInMemory.GetUserInfoByName(id);
            if (userInfo != null)
            {
                _userInfoInMemory.Remove(userInfo);

                await Clients.Client(userInfo.ConnectionId)
                       .ForceLogout(userInfo);

                await Clients.AllExcept([userInfo.ConnectionId])
                    .UserLeft(id);
            }
        }

        public async Task Join(SignlarUser userInfo)
        {
            if (!_userInfoInMemory.AddUpdate(userInfo, Context.ConnectionId))
            {
                // new user
                await Clients.AllExcept([Context.ConnectionId])
                    .NewOnlineUser(_userInfoInMemory.GetUserInfo(userInfo));
            }
            else
            {
                // existing user joined again
            }

            await Clients.Client(Context.ConnectionId)
                .Joined(_userInfoInMemory.GetUserInfo(userInfo));

            await Clients.Client(Context.ConnectionId)
                .OnlineUsers(_userInfoInMemory.GetAllUsersExceptThis(userInfo));
        }

        public Task SendDirectMessage(string message, string targetUserName)
        {
            var userInfoSender = _userInfoInMemory.GetUserInfoByConnectionId(Context.ConnectionId);
            var userInfoReciever = _userInfoInMemory.GetUserInfoByName(targetUserName);
            return Clients.Client(userInfoReciever.ConnectionId).SendDM(message, userInfoSender);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userInfo = _userInfoInMemory.GetUserInfoByConnectionId(Context.ConnectionId);
            if (userInfo == null)
                return;
            _userInfoInMemory.Remove(userInfo);
            await Clients.AllExcept([userInfo.ConnectionId]).UserLeft(userInfo.Id);
        }
    }
}
