using UserManagement.MediatR.Commands;
using FluentValidation;
using System;

namespace UserManagement.MediatR.Validators
{
    public class UpdatePageCommandValidator : AbstractValidator<UpdatePageCommand>
    {
        public UpdatePageCommandValidator()
        {
            RuleFor(c => c.Id).Must(NotEmptyGuid).WithMessage("Id is required");
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required");
        }

        private bool NotEmptyGuid(Guid p)
        {
            return p != Guid.Empty;
        }
    }
}
