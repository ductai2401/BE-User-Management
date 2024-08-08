using AutoMapper;
using UserManagement.Common.UnitOfWork;
using UserManagement.Data;
using UserManagement.Data.Dto;
using UserManagement.Domain;
using UserManagement.MediatR.Commands;
using UserManagement.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Helper;

namespace UserManagement.MediatR.Handlers
{
    public class UpdateUserClaimCommandHandler(
        IMapper mapper,
        IUserClaimRepository userClaimRepository,
        IUnitOfWork<UserContext> uow
        ) : IRequestHandler<UpdateUserClaimCommand, ServiceResponse<UserClaimDto>>
    {
        public async Task<ServiceResponse<UserClaimDto>> Handle(UpdateUserClaimCommand request, CancellationToken cancellationToken)
        {
            var appUserClaims = await userClaimRepository.All.Where(c => c.UserId == request.Id).ToListAsync(cancellationToken);
            var claimsToAdd = request.UserClaims.Where(c => !appUserClaims.Select(c => c.ClaimType).Contains(c.ClaimType)).ToList();
            userClaimRepository.AddRange(mapper.Map<List<UserClaim>>(claimsToAdd));
            var claimsToDelete = appUserClaims.Where(c => !request.UserClaims.Select(cs => cs.ClaimType).Contains(c.ClaimType)).ToList();
            userClaimRepository.RemoveRange(claimsToDelete);
            if (await uow.SaveAsync(cancellationToken) <= 0)
            {
                return ServiceResponse<UserClaimDto>.Return500();
            }
            return ServiceResponse<UserClaimDto>.ReturnSuccess();
        }
    }
}
