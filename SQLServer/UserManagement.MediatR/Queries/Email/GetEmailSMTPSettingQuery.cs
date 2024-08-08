using MediatR;
using System;
using UserManagement.Data.Dto;
using UserManagement.Helper;

namespace UserManagement.MediatR.Queries
{
    public class GetEmailSMTPSettingQuery : IRequest<ServiceResponse<EmailSMTPSettingDto>>
    {
        public Guid Id { get; set; }
    }
}
