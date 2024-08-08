using UserManagement.MediatR.Commands;
using FluentValidation;

namespace UserManagement.MediatR.Validators
{
    public class AddActionCommandValidator: AbstractValidator<AddActionCommand>
    {
        public AddActionCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required");
        }
    }
}
