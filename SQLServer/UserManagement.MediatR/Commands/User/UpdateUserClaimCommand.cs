using UserManagement.Data.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using UserManagement.Helper;

namespace UserManagement.MediatR.Commands
{
    public class UpdateUserClaimCommand : IRequest<ServiceResponse<UserClaimDto>>
    {
        public Guid Id { get; set; }
        public List<UserClaimDto> UserClaims { get; set; }
    }
}
