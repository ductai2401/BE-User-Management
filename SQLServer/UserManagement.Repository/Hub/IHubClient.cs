using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Data.Dto;
using UserManagement.Data.Dto.User;

namespace UserManagement.Repository
{
    public interface IHubClient
    {
        Task UserLeft(string id);

        Task NewOnlineUser(SignlarUser userInfo);

        Task Joined(SignlarUser userInfo);

        Task OnlineUsers(IEnumerable<SignlarUser> userInfo);

        Task Logout(SignlarUser userInfo);

        Task ForceLogout(SignlarUser userInfo);

        Task SendDM(string message, SignlarUser userInfo);


    }
}
