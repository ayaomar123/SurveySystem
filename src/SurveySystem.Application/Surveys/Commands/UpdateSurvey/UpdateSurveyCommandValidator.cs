using FluentValidation;

namespace SurveySystem.Application.Surveys.Commands.UpdateSurvey
{
    public sealed class UpdateSurveyCommandValidator : AbstractValidator<UpdateSurveyCommand>
    {
        public UpdateSurveyCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Status)
                .IsInEnum();

            RuleFor(x => x.StartDate)
                .LessThan(x => x.EndDate)
                .When(x => x.StartDate.HasValue && x.EndDate.HasValue)
                .WithMessage("StartDate must be before EndDate.");
        }
    }
}
