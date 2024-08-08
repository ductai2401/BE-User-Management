using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Data.Dto;
using UserManagement.MediatR.Queries;
using UserManagement.Repository;

namespace UserManagement.MediatR.Handlers
{
    public class GetRecentlyRegisteredUserQueryHandler(
       IUserRepository userRepository,
       IMapper mapper
        ) : IRequestHandler<GetRecentlyRegisteredUserQuery, List<UserDto>>
    {
        public async Task<List<UserDto>> Handle(GetRecentlyRegisteredUserQuery request, CancellationToken cancellationToken)
        {
            var entities = await userRepository.All.OrderByDescending(c => c.CreatedDate).Take(10).ToListAsync(cancellationToken);
            return mapper.Map<List<UserDto>>(entities);
        }
    }
}
