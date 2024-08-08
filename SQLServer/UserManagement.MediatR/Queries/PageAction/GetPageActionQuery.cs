using UserManagement.Data.Dto;
using MediatR;
using System;
using UserManagement.Helper;

namespace UserManagement.MediatR.Queries
{
    public class GetPageActionQuery : IRequest<ServiceResponse<PageActionDto>>
    {
        public Guid Id { get; set; }
    }
}
