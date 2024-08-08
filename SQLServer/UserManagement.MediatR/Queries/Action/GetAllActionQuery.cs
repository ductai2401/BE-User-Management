using UserManagement.Data.Dto;
using MediatR;
using System.Collections.Generic;
using UserManagement.Helper;

namespace UserManagement.MediatR.Queries
{
    public class GetAllActionQuery : IRequest<ServiceResponse<List<ActionDto>>>
    {
    }
}
