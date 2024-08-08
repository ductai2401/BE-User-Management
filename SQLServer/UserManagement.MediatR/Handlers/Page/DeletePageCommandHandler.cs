using UserManagement.Common.UnitOfWork;
using UserManagement.Data.Dto;
using UserManagement.Domain;
using UserManagement.MediatR.Commands;
using UserManagement.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Helper;

namespace UserManagement.MediatR.Handlers
{
    public class DeletePageCommandHandler(
        IPageRepository pageRepository,
        IUnitOfWork<UserContext> uow
        ) : IRequestHandler<DeletePageCommand, ServiceResponse<PageDto>>
    {
        public async Task<ServiceResponse<PageDto>> Handle(DeletePageCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await pageRepository.FindAsync(request.Id, cancellationToken);
            if (entityExist == null)
            {
                return ServiceResponse<PageDto>.Return404();
            }
            pageRepository.Delete(request.Id);
            if (await uow.SaveAsync(cancellationToken) <= 0)
            {
                return ServiceResponse<PageDto>.Return500();
            }
            return ServiceResponse<PageDto>.ReturnSuccess();
        }
    }
}
