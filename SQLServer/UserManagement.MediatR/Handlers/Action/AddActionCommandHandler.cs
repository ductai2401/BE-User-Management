using AutoMapper;
using UserManagement.Common.UnitOfWork;
using UserManagement.Data.Dto;
using UserManagement.Domain;
using UserManagement.MediatR.Commands;
using UserManagement.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Helper;
using Microsoft.Extensions.Logging;

namespace UserManagement.MediatR.Handlers
{
    public class AddActionCommandHandler(
        IActionRepository actionRepository,
        IMapper mapper,
        IUnitOfWork<UserContext> uow,
        ILogger<AddActionCommandHandler> logger
        ) : IRequestHandler<AddActionCommand, ServiceResponse<ActionDto>>
    {
        public async Task<ServiceResponse<ActionDto>> Handle(AddActionCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await actionRepository.FindBy(c => c.Name.Trim().ToLower() == request.Name.Trim().ToLower())
                .FirstOrDefaultAsync(cancellationToken);
            if (entityExist != null)
            {
                logger.LogError("Action already exist.");
                return ServiceResponse<ActionDto>.Return409("Action already exist.");
            }
            var entity = mapper.Map<Data.Action>(request);
            entity.Id = Guid.NewGuid();
            actionRepository.Add(entity);
            if (await uow.SaveAsync(cancellationToken) <= 0)
            {
                return ServiceResponse<ActionDto>.Return500();
            }
            var entityDto = mapper.Map<ActionDto>(entity);
            return ServiceResponse<ActionDto>.ReturnResultWith200(entityDto);
        }
    }
}
