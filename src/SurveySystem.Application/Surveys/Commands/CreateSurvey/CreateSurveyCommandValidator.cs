using FluentValidation;
using SurveySystem.Application.Surveys.Commands.CreateSurvey;

namespace SurveySystem.Application.Surveys.Commands.CreateSurvey
{
    public sealed class CreateSurveyCommandValidator
        : AbstractValidator<CreateSurveyCommand>
    {
        public CreateSurveyCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title cannot exced 200");

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Invalid survey status.");

            RuleFor(x => x.StartDate)
                .LessThan(x => x.EndDate)
                .When(x => x.StartDate.HasValue && x.EndDate.HasValue)
                .WithMessage("StartDate must be before EndDate.");

            RuleFor(x => x.Questions)
                .NotNull().WithMessage("At least one question must be selected.")
                .NotEmpty().WithMessage("At least one question must be selected.");

            RuleForEach(x => x.Questions).ChildRules(questions =>
            {
                questions.RuleFor(q => q.QuestionId)
                    .NotEmpty().WithMessage("QuestionId cannot be empty.");

                questions.RuleFor(q => q.Order)
                    .GreaterThan(0).WithMessage("Order must be greater than zero.");
            });
        }
    }
}
