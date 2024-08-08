using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Common.UnitOfWork;
using UserManagement.Data;
using UserManagement.Data.Dto;
using UserManagement.Domain;
using UserManagement.Helper;
using UserManagement.MediatR.Commands;
using UserManagement.Repository;

namespace UserManagement.MediatR.Handlers
{
    public class AddLogCommandHandler(
        INLogRespository nLogRespository,
        IUnitOfWork<UserContext> uow
        ) : IRequestHandler<AddLogCommand, ServiceResponse<NLogDto>>
    {
        public async Task<ServiceResponse<NLogDto>> Handle(AddLogCommand request, CancellationToken cancellationToken)
        {
            nLogRespository.Add(new NLog
            {
                Id = Guid.NewGuid(),
                Logged = DateTime.Now.ToLocalTime(),
                Level = "Error",
                Message = request.ErrorMessage,
                Source = "Angular",
                Exception = request.Stack
            });

            await uow.SaveAsync(cancellationToken);
            return ServiceResponse<NLogDto>.ReturnSuccess();
        }
    }
}
