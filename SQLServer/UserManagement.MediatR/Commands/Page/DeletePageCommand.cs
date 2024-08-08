using UserManagement.Data.Dto;
using MediatR;
using System;
using UserManagement.Helper;

namespace UserManagement.MediatR.Commands
{
    public class DeletePageCommand : IRequest<ServiceResponse<PageDto>>
    {
        public Guid Id { get; set; }
    }
}
