using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Data.Dto;
using UserManagement.Data.Dto.User;
using UserManagement.Helper;
using UserManagement.MediatR.Queries;
using UserManagement.Repository;

namespace UserManagement.MediatR.Handlers
{
    public class GetOnlineUsersQueryHandler(IUserRepository userRepository,
        IConnectionMappingRepository connectionMappingRepository,
        PathHelper pathHelper,
        UserInfoToken userInfoToken) : IRequestHandler<GetOnlineUsersQuery, List<UserDto>>
    {
        public async Task<List<UserDto>> Handle(GetOnlineUsersQuery request, CancellationToken cancellationToken)
        {
            var user = new SignlarUser
            {
                ConnectionId = userInfoToken.ConnectionId,
                Email = userInfoToken.Email,
                Id = userInfoToken.Id,
            };

            var allUserIds = connectionMappingRepository.GetAllUsersExceptThis(user).Select(c => Guid.Parse(c.Id)).ToList();
            var users = await userRepository.All.Where(c => allUserIds.Contains(c.Id))
                .Select(cs => new UserDto
                {
                    Id = cs.Id,
                    FirstName = cs.FirstName,
                    LastName = cs.LastName,
                    Email = cs.Email,
                    ProfilePhoto = !string.IsNullOrWhiteSpace(cs.ProfilePhoto) ? $"{pathHelper.UserProfilePath}{cs.ProfilePhoto}" : string.Empty
                }).ToListAsync(cancellationToken);
            return users;
        }
    }
}
