using AutoMapper;
using UserManagement.Data;
using UserManagement.Data.Dto;
using UserManagement.MediatR.Commands;

namespace UserManagement.API.Helpers.Mapping
{
    public class PageProfile : Profile
    {
        public PageProfile()
        {
            CreateMap<Page, PageDto>().ReverseMap();
            CreateMap<AddPageCommand, Page>();
            CreateMap<UpdatePageCommand, Page>();
        }
    }
}
