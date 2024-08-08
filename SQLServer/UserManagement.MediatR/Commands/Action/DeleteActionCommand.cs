using UserManagement.Data.Dto;
using MediatR;
using System;
using UserManagement.Helper;

namespace UserManagement.MediatR.Commands
{
    public class DeleteActionCommand : IRequest<ServiceResponse<ActionDto>>
    {
        public Guid Id { get; set; }
    }
}
