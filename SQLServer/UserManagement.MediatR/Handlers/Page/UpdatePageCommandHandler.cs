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
    public class UpdatePageCommandHandler(
        IPageRepository pageRepository,
        IMapper mapper,
        IUnitOfWork<UserContext> uow,
        ILogger<UpdatePageCommandHandler> logger
        ) : IRequestHandler<UpdatePageCommand, ServiceResponse<PageDto>>
    {
        public async Task<ServiceResponse<PageDto>> Handle(UpdatePageCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await pageRepository.FindBy(c => c.Name == request.Name && c.Id != request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (entityExist != null)
            {
                logger.LogError("Page Name Already Exist.");
                return ServiceResponse<PageDto>.Return409("Page Name Already Exist.");
            }
            entityExist = await pageRepository.FindBy(v => v.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
            entityExist.Name = request.Name;
            entityExist.Url = request.Url;
            pageRepository.Update(entityExist);
            if (await uow.SaveAsync(cancellationToken) <= 0)
            {
                return ServiceResponse<PageDto>.Return500();
            }
            return ServiceResponse<PageDto>.ReturnResultWith200(mapper.Map<PageDto>(entityExist));
        }
    }
}
