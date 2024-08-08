using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Data.Dto;
using UserManagement.Helper;
using UserManagement.MediatR.Commands;
using UserManagement.Repository;

namespace UserManagement.MediatR.Handlers
{
    public class SendEmailCommandHandler(
        IEmailSMTPSettingRepository emailSMTPSettingRepository,
        ILogger<SendEmailCommandHandler> logger,
        UserInfoToken userInfoToken
        ) : IRequestHandler<SendEmailCommand, ServiceResponse<EmailDto>>
    {
        public async Task<ServiceResponse<EmailDto>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            var defaultSmtp = await emailSMTPSettingRepository.FindBy(c => c.IsDefault).FirstOrDefaultAsync(cancellationToken);
            if (defaultSmtp == null)
            {
                logger.LogError("Default SMTP setting does not exist.");
                return ServiceResponse<EmailDto>.Return404("Default SMTP setting does not exist.");
            }
            try
            {
                EmailHelper.SendEmail(new SendEmailSpecification
                {
                    Body = request.Body,
                    FromAddress = userInfoToken.Email,
                    Host = defaultSmtp.Host,
                    IsEnableSSL = defaultSmtp.IsEnableSSL,
                    Password = defaultSmtp.Password,
                    Port = defaultSmtp.Port,
                    Subject = request.Subject,
                    ToAddress = request.ToAddress,
                    CCAddress = request.CCAddress,
                    UserName = defaultSmtp.UserName,
                    Attechments = request.Attechments
                });
                return ServiceResponse<EmailDto>.ReturnSuccess();
            }
            catch (Exception e)
            {
                logger.LogError(e.Message, e);
                return ServiceResponse<EmailDto>.ReturnFailed(500, e.Message);
            }
        }
    }
}
