using AutoMapper;
using UserManagement.Data;
using UserManagement.Data.Dto;
using UserManagement.MediatR.Commands;

namespace UserManagement.API.Helpers.Mapping
{
    public class ActionProfile : Profile
    {
        public ActionProfile()
        {
            CreateMap<Action, ActionDto>().ReverseMap();
            CreateMap<AddActionCommand, Action>();
            CreateMap<UpdateActionCommand, Action>();
        }
    }
}
