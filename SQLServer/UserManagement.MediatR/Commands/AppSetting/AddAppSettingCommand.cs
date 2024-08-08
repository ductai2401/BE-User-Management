using MediatR;
using UserManagement.Data.Dto;
using UserManagement.Helper;

namespace UserManagement.MediatR.Commands
{
    public class AddAppSettingCommand: IRequest<ServiceResponse<AppSettingDto>>
    {
        public string Name { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
