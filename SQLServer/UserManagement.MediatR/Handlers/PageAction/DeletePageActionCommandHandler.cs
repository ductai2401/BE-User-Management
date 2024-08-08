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

namespace UserManagement.MediatR.Handlers
{
    public class DeletePageActionCommandHandler(
        IPageActionRepository pageActionRepository,
        IUnitOfWork<UserContext> uow
        ) : IRequestHandler<DeletePageActionCommand, ServiceResponse<PageActionDto>>
    {
        public async Task<ServiceResponse<PageActionDto>> Handle(DeletePageActionCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await pageActionRepository.FindBy(c => c.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
            if (entityExist == null)
            {
                return ServiceResponse<PageActionDto>.Return404();
            }
            pageActionRepository.Remove(entityExist);
            if (await uow.SaveAsync(cancellationToken) <= 0)
            {
                return ServiceResponse<PageActionDto>.Return500();
            }
            return ServiceResponse<PageActionDto>.ReturnSuccess();
        }
    }
}
