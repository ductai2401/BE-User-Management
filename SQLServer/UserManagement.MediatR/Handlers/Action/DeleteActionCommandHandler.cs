using UserManagement.Common.UnitOfWork;
using UserManagement.Data.Dto;
using UserManagement.Domain;
using UserManagement.MediatR.Commands;
using UserManagement.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Helper;
using Microsoft.Extensions.Logging;

namespace UserManagement.MediatR.Handlers
{
    public class DeleteActionCommandHandler(
        IActionRepository actionRepository,
        IUnitOfWork<UserContext> uow,
        ILogger<DeleteActionCommandHandler> logger
        ) : IRequestHandler<DeleteActionCommand, ServiceResponse<ActionDto>>
    {
        public async Task<ServiceResponse<ActionDto>> Handle(DeleteActionCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await actionRepository.FindAsync(request.Id, cancellationToken);
            if (entityExist == null)
            {
                logger.LogError("Not found");
                return ServiceResponse<ActionDto>.Return404();
            }

            actionRepository.Delete(request.Id);

            if (await uow.SaveAsync(cancellationToken) <= 0)
            {
                return ServiceResponse<ActionDto>.Return500();
            }

            return ServiceResponse<ActionDto>.ReturnSuccess();
        }
    }
}
