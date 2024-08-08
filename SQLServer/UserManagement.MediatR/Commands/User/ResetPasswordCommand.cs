using UserManagement.Data.Dto;
using MediatR;
using UserManagement.Helper;

namespace UserManagement.MediatR.Commands
{
    public class ResetPasswordCommand : IRequest<ServiceResponse<UserDto>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
