using UserManagement.MediatR.Commands;
using FluentValidation;

namespace UserManagement.MediatR.Validators
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(c => c.UserName).NotEmpty().WithMessage("UserName is required");
            RuleFor(c => c.Password).NotEmpty().WithMessage("Password is required");
        }
    }
}
