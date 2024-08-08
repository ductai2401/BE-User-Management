using AutoMapper;
using UserManagement.Common.UnitOfWork;
using UserManagement.Data;
using UserManagement.Data.Dto;
using UserManagement.Domain;
using UserManagement.MediatR.Commands;
using UserManagement.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Helper;

namespace UserManagement.MediatR.Handlers
{
    public class AddPageCommandHandler(
        IPageRepository pageRepository,
        IMapper mapper,
        IUnitOfWork<UserContext> uow,
        ILogger<AddPageCommandHandler> logger
        ) : IRequestHandler<AddPageCommand, ServiceResponse<PageDto>>
    {
        public async Task<ServiceResponse<PageDto>> Handle(AddPageCommand request, CancellationToken cancellationToken)
        {
            var existingEntity = await pageRepository.FindBy(c => c.Name == request.Name).FirstOrDefaultAsync(cancellationToken);
            if (existingEntity != null)
            {
                logger.LogError("Page Already Exist");
                return ServiceResponse<PageDto>.Return409("Page Already Exist.");
            }
            var entity = mapper.Map<Page>(request);
            pageRepository.Add(entity);
            if (await uow.SaveAsync(cancellationToken) <= 0)
            {

                logger.LogError("Save Page have Error");
                return ServiceResponse<PageDto>.Return500();
            }
            return ServiceResponse<PageDto>.ReturnResultWith200(mapper.Map<PageDto>(entity));
        }
    }
}
