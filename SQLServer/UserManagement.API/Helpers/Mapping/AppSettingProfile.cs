using AutoMapper;
using UserManagement.Data;
using UserManagement.Data.Dto;
using UserManagement.MediatR.Commands;

namespace UserManagement.API.Helpers.Mapping
{
    public class AppSettingProfile : Profile
    {
        public AppSettingProfile()
        {
            CreateMap<AppSettingDto, AppSetting>().ReverseMap();
            CreateMap<AddAppSettingCommand, AppSetting>();
            CreateMap<UpdateAppSettingCommand, AppSetting>().ReverseMap();

        }
    }
}
