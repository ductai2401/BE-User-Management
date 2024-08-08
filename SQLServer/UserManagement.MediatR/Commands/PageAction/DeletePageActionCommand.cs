using UserManagement.Data.Dto;
using MediatR;
using System;
using UserManagement.Helper;

namespace UserManagement.MediatR.Commands
{
    public class DeletePageActionCommand : IRequest<ServiceResponse<PageActionDto>>
    {
        public Guid Id { get; set; }
        public Guid PageId { get; set; }
        public Guid ActionId { get; set; }
    }
}
