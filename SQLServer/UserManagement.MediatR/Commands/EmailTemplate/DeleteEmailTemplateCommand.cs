using MediatR;
using System;
using UserManagement.Helper;

namespace UserManagement.MediatR.Commands
{
    public class DeleteEmailTemplateCommand : IRequest<ServiceResponse<bool>>
    {
        public Guid Id { get; set; }
    }
}
