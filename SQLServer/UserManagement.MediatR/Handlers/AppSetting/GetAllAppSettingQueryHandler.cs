using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Data.Dto;
using UserManagement.Helper;
using UserManagement.MediatR.Queries;
using UserManagement.Repository;

namespace UserManagement.MediatR.Handlers
{
    public class GetAllAppSettingQueryHandler(
        IAppSettingRepository appSettingRepository,
        IMapper mapper
        ) : IRequestHandler<GetAllAppSettingQuery, ServiceResponse<List<AppSettingDto>>>
    {
        public async Task<ServiceResponse<List<AppSettingDto>>> Handle(GetAllAppSettingQuery request, CancellationToken cancellationToken)
        {
            var entities = await appSettingRepository.All.ToListAsync(cancellationToken);
            return ServiceResponse<List<AppSettingDto>>.ReturnResultWith200(mapper.Map<List<AppSettingDto>>(entities));
        }
    }
}
