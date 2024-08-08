using MediatR;
using UserManagement.Data.Dto;
using UserManagement.Helper;

namespace UserManagement.MediatR.Commands
{
    public class AddEmailTemplateCommand : IRequest<ServiceResponse<EmailTemplateDto>>
    {
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
