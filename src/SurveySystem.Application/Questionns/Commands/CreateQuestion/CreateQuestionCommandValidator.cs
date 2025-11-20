using FluentValidation;
using SurveySystem.Application.Questions.Dtos;

namespace SurveySystem.Application.Questionns.Commands.CreateQuestion
{
    public sealed class CreateQuestionCommandValidator : AbstractValidator<CreateQuestionCommand>
    {
        public CreateQuestionCommandValidator()
        {
            RuleFor(x => x.Request.Title)
                .NotEmpty().WithMessage("Question text is required.")
                .MinimumLength(3).WithMessage("Question must at least be 3");

            RuleFor(x => x.Request.Description)
                .MinimumLength(10).WithMessage("Description must at least be 3");

            RuleFor(x => x.Request.QuestionType)
                .IsInEnum();

            When(x => x.Request.QuestionType is QuestionTypeDto.Radio or QuestionTypeDto.Checkbox, () =>
            {
                RuleFor(x => x.Request.Choices)
                    .NotNull()
                    .Must(c => c!.Count > 0)
                    .WithMessage("Choices are required for Radio/Checkbox questions.");
            });

            When(x => x.Request.QuestionType == QuestionTypeDto.Slider, () =>
            {
                RuleFor(x => x.Request.Config)
                    .NotNull()
                    .WithMessage("Slider config is required for Slider question.");
            });

            When(x => x.Request.QuestionType == QuestionTypeDto.Rating, () =>
            {
                RuleFor(x => x.Request.StarConfig)
                    .NotNull()
                    .WithMessage("Star config is required for Rating question.");
            });

        }
    }
}
