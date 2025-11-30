using FluentValidation;
using SurveySystem.Application.Surveys.Responses.Dtos;

namespace SurveySystem.Application.Surveys.Responses.Commands.SubmitSurveyResponse
{
    public class SubmitSurveyResponseCommandValidator
        : AbstractValidator<SubmitSurveyResponseCommand>
    {
        public SubmitSurveyResponseCommandValidator()
        {
            RuleFor(x => x.SurveyId)
                .NotEmpty().WithMessage("SurveyId is required.");

            RuleFor(x => x.Answers)
                .NotEmpty().WithMessage("At least one answer is required.");

            RuleForEach(x => x.Answers).ChildRules(answer =>
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

        private static bool ValidateSingleAnswer(SubmitAnswerDto a)
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
