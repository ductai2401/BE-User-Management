using UserManagement.Data.Dto;
using MediatR;
using System.Collections.Generic;

namespace UserManagement.MediatR.Queries
{
    public class GetAllPageQuery : IRequest<List<PageDto>>
    {
    }
}
