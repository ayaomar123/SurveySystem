using FluentValidation;

namespace SurveySystem.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Request.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(x => x.Request.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email is required.");

            RuleFor(x => x.Request.PasswordHash)
                .NotEmpty().WithMessage("PasswordHash is required.")
                .MinimumLength(6).WithMessage("PasswordHash must be at least 6 characters long.");

            RuleFor(x => x.Request.Role)
                .IsInEnum().WithMessage("Role must be a valid UserRole.");
        }
    }
}
