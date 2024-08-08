using UserManagement.Data.Dto;
using MediatR;
using System;
using UserManagement.Helper;

namespace UserManagement.MediatR.Queries
{
    public class GetPageQuery : IRequest<ServiceResponse<PageDto>>
    {
        public Guid Id { get; set; }
    }
}
