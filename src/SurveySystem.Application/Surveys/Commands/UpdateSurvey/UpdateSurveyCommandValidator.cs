using FluentValidation;

namespace SurveySystem.Application.Surveys.Commands.UpdateSurvey
{
    public sealed class UpdateSurveyCommandValidator : AbstractValidator<UpdateSurveyCommand>
    {
        public UpdateSurveyCommandValidator()
        {
            RuleFor(x => x.Request.Id)
                .NotEmpty();

            RuleFor(x => x.Request.Title)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Request.Status)
                .IsInEnum();

            RuleFor(x => x.Request.StartDate)
                .LessThan(x => x.Request.EndDate)
                .When(x => x.Request.StartDate.HasValue && x.Request.EndDate.HasValue)
                .WithMessage("StartDate > EndDate");
        }
    }
}
