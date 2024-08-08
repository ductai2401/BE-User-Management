using UserManagement.Data.Dto;
using MediatR;
using System;
using UserManagement.Helper;

namespace UserManagement.MediatR.Commands
{
    public class AddPageActionCommand: IRequest<ServiceResponse<PageActionDto>>
    {
        public Guid PageId { get; set; }
        public Guid ActionId { get; set; }
        public bool Flag { get; set; }
    }
}
