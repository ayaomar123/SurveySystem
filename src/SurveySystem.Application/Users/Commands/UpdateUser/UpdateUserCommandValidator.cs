
using FluentValidation;

namespace SurveySystem.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.Id)
                  .NotEmpty().WithMessage("ID is required.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email is required.");

            RuleFor(x => x.PasswordHash)
                .NotEmpty().WithMessage("PasswordHash is required.")
                .MinimumLength(6).WithMessage("PasswordHash must be at least 6 characters long.");

            RuleFor(x => x.Role)
                .IsInEnum().WithMessage("Role must be a valid UserRole.");
        }
    }
}
