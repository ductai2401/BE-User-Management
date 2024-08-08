using UserManagement.Data.Dto;
using MediatR;
using UserManagement.Helper;

namespace UserManagement.MediatR.Commands
{
    public class ChangePasswordCommand : IRequest<ServiceResponse<UserDto>>
    {
        public string UserName { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
