using MediatR;
using UserManagement.Data.Dto;
using UserManagement.Helper;

namespace UserManagement.MediatR.Queries
{
    public class GetAppSettingByKeyQuery : IRequest<ServiceResponse<AppSettingDto>>
    {
        public string Key { get; set; }
    }
}
