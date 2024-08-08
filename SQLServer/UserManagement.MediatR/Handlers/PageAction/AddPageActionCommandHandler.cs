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
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Helper;

namespace UserManagement.MediatR.Handlers
{
    public class AddPageActionCommandHandler(
        IPageActionRepository pageActionRepository,
        IMapper mapper,
        IUnitOfWork<UserContext> uow
        ) : IRequestHandler<AddPageActionCommand, ServiceResponse<PageActionDto>>
    {
        public async Task<ServiceResponse<PageActionDto>> Handle(AddPageActionCommand request, CancellationToken cancellationToken)
        {
            var entity = await pageActionRepository
                .FindBy(c => c.PageId == request.PageId && c.ActionId == request.ActionId)
                .FirstOrDefaultAsync(cancellationToken);
            if (entity == null)
            {
                entity = mapper.Map<PageAction>(request);
                entity.Id = Guid.NewGuid();
                pageActionRepository.Add(entity);
                if (await uow.SaveAsync(cancellationToken) <= 0)
                {
                    return ServiceResponse<PageActionDto>.Return500();
                }
            }
            return ServiceResponse<PageActionDto>.ReturnResultWith200(mapper.Map<PageActionDto>(entity));
        }
    }
}
