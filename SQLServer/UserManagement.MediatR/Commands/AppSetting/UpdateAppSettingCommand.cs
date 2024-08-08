using MediatR;
using System;
using UserManagement.Data.Dto;
using UserManagement.Helper;

namespace UserManagement.MediatR.Commands
{
    public class UpdateAppSettingCommand : IRequest<ServiceResponse<AppSettingDto>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
