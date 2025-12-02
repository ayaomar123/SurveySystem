using FluentValidation;
using SurveySystem.Application.Surveys.Commands.CreateSurvey;

namespace SurveySystem.Application.Surveys.Commands.CreateSurvey
{
    public sealed class CreateSurveyCommandValidator
        : AbstractValidator<CreateSurveyCommand>
    {
        public CreateSurveyCommandValidator()
        {
            RuleFor(x =>  x.Request.Title)
                .NotEmpty().WithMessage("Title is required")
                .MinimumLength(3).WithMessage("Title min 3")
                .MaximumLength(200).WithMessage("Title max 200");

            RuleFor(x =>  x.Request.Status)
                .IsInEnum().WithMessage("Invalid survey status");

            RuleFor(x =>  x.Request.StartDate)
                .LessThan(x =>  x.Request.EndDate)
                .When(x =>  x.Request.StartDate.HasValue &&  x.Request.EndDate.HasValue)
                .WithMessage("StartDate > EndDate");

            RuleFor(x =>  x.Request.Questions)
                .NotNull().WithMessage("Questions required")
                .NotEmpty().WithMessage("Questions required");

            RuleForEach(x =>  x.Request.Questions).ChildRules(questions =>
            {
                questions.RuleFor(q => q.QuestionId)
                    .NotEmpty().WithMessage("QuestionId required");

                questions.RuleFor(q => q.Order)
                    .GreaterThan(0).WithMessage("Order must > 0");
            });
        }
    }
}
