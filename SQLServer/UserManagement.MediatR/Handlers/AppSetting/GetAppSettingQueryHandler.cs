using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Data.Dto;
using UserManagement.Helper;
using UserManagement.MediatR.Queries;
using UserManagement.Repository;

namespace UserManagement.MediatR.Handlers
{
    public class GetAppSettingQueryHandler(
        IAppSettingRepository appSettingRepository,
        IMapper mapper,
        ILogger<GetAppSettingByKeyQueryHandler> logger
        ) : IRequestHandler<GetAppSettingQuery, ServiceResponse<AppSettingDto>>
    {
        public async Task<ServiceResponse<AppSettingDto>> Handle(GetAppSettingQuery request, CancellationToken cancellationToken)
        {
            var appsetting = await appSettingRepository.FindBy(c => c.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
            if (appsetting == null)
            {
                logger.LogError("AppSetting key is not available");
                return ServiceResponse<AppSettingDto>.Return404();
            }
            return ServiceResponse<AppSettingDto>.ReturnResultWith200(mapper.Map<AppSettingDto>(appsetting));
        }
    }
}
