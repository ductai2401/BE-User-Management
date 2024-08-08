using UserManagement.MediatR.Commands;
using FluentValidation;

namespace UserManagement.MediatR.Validators
{
    public class AddPageCommandValidator:  AbstractValidator<AddPageCommand>
    {
        public AddPageCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required");
        }
    }
}
