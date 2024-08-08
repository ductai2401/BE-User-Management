using MediatR;
using System.Collections.Generic;
using UserManagement.Data.Dto;
using UserManagement.Helper;

namespace UserManagement.MediatR.Queries
{
    public class GetAllAppSettingQuery : IRequest<ServiceResponse<List<AppSettingDto>>>
    {
    }
}
