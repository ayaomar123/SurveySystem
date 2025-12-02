using FluentValidation;
using SurveySystem.Application.Surveys.Responses.Commands.SubmitSurvey.Dtos;

namespace SurveySystem.Application.Surveys.Responses.Commands.SubmitSurvey
{
    public class SubmitSurveyCommandValidator
        : AbstractValidator<SubmitSurveyCommand>
    {
        public SubmitSurveyCommandValidator()
        {
            RuleFor(x => x.Request.SurveyId)
                .NotEmpty().WithMessage("SurveyId is required.");

            RuleFor(x => x.Request.Answers)
                .NotEmpty().WithMessage("At least one answer is required.");

            RuleForEach(x => x.Request.Answers).ChildRules(answer =>
            {
                answer.RuleFor(a => a.QuestionId)
                      .NotEmpty().WithMessage("QuestionId is required.");

                answer.RuleFor(a => a)
                      .Must(a => ValidateSingleAnswer(a))
                      .WithMessage("Invalid answer. Provide only one type of answer: Value OR SelectedChoiceId OR SelectedChoices.");

                answer.When(a => !string.IsNullOrWhiteSpace(a.Value), () =>
                {
                    answer.RuleFor(a => a.Value)
                          .NotEmpty().WithMessage("Value cannot be empty.");
                });

               

                answer.When(a => a.SelectedChoicesIds != null, () =>
                {
                    answer.RuleFor(a => a.SelectedChoicesIds!)
                          .NotEmpty().WithMessage("SelectedChoices cannot be empty.");
                });
            });
        }

        private static bool ValidateSingleAnswer(SubmitSurveyAnswerDto a)
        {
            int count = 0;

            if (!string.IsNullOrWhiteSpace(a.Value))
                count++;

            if (a.SelectedChoiceId.HasValue)
                count++;

            if (a.SelectedChoicesIds is { Count: > 0 })
                count++;

            return count == 1;
        }
    }
}
