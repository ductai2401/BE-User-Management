using FluentValidation;
using UserManagement.MediatR.Commands;

namespace UserManagement.MediatR.Validators
{
    public class UpdateAppSettingCommandValidator : AbstractValidator<UpdateAppSettingCommand>
    {
        public UpdateAppSettingCommandValidator()
        {
          
            RuleFor(c => c.Id).NotEmpty().WithMessage("Id is required");
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(c => c.Key).NotEmpty().WithMessage("Key is required");
            RuleFor(c => c.Value).NotEmpty().WithMessage("Value is required");
        }
    }
}