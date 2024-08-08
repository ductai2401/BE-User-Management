using MediatR;
using System.Collections.Generic;
using UserManagement.Data.Dto;

namespace UserManagement.MediatR.Queries
{
    public class GetEmailSMTPSettingsQuery : IRequest<List<EmailSMTPSettingDto>>
    {
    }
}
