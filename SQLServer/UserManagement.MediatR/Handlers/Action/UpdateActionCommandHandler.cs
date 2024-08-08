using AutoMapper;
using UserManagement.Common.UnitOfWork;
using UserManagement.Data.Dto;
using UserManagement.Domain;
using UserManagement.MediatR.Commands;
using UserManagement.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Helper;
using Microsoft.Extensions.Logging;

namespace UserManagement.MediatR.Handlers
{
    public class UpdateActionCommandHandler(
        IActionRepository actionRepository,
        IMapper mapper,
        IUnitOfWork<UserContext> uow,
        ILogger<UpdateActionCommandHandler> logger
        ) : IRequestHandler<UpdateActionCommand, ServiceResponse<ActionDto>>
    {
        public async Task<ServiceResponse<ActionDto>> Handle(UpdateActionCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await actionRepository.FindBy(c => c.Name == request.Name && c.Id != request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (entityExist != null)
            {
                logger.LogError("Action Name Already Exist.");
                return ServiceResponse<ActionDto>.Return409("Action Name Already Exist.");
            }

            entityExist = await actionRepository.FindBy(v => v.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
            entityExist.Name = request.Name;
            actionRepository.Update(entityExist);
            
            if (await uow.SaveAsync(cancellationToken) <= 0)
            {
                return ServiceResponse<ActionDto>.Return500();
            }

            var entityDto = mapper.Map<ActionDto>(entityExist);
            return ServiceResponse<ActionDto>.ReturnSuccess();
        }
    }
}
