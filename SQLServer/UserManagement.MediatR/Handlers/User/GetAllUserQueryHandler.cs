using AutoMapper;
using UserManagement.Data.Dto;
using UserManagement.MediatR.Queries;
using UserManagement.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace UserManagement.MediatR.Handlers
{
    public class GetAllUserQueryHandler(
       IUserRepository userRepository,
       IMapper mapper
        ) : IRequestHandler<GetAllUserQuery, List<UserDto>>
    {
        public async Task<List<UserDto>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            var entities = await userRepository.All.ToListAsync(cancellationToken);
            return mapper.Map<List<UserDto>>(entities);
        }
    }
}
