using AutoMapper;
using UserManagement.Data;
using UserManagement.Data.Dto;
using UserManagement.MediatR.Commands;

namespace UserManagement.API.Helpers.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserClaimDto, UserClaim>().ReverseMap();
            CreateMap<UserRoleDto, UserRole>().ReverseMap();
            CreateMap<UserAllowedIPDto, UserAllowedIP>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<AddUserCommand, User>();
            CreateMap<SocialLoginCommand, User>();
            CreateMap<ResetPasswordCommand, UserDto>();
        }
    }
}
