using FluentValidation;
using SurveySystem.Application.Questions.Dtos;

namespace SurveySystem.Application.Questionns.Commands.CreateQuestion
{
    public sealed class CreateQuestionCommandValidator : AbstractValidator<CreateQuestionCommand>
    {
        public CreateQuestionCommandValidator()
        {
            RuleFor(x => x.Request.Title)
                .NotEmpty().WithMessage("Title required.")
                .MinimumLength(3).WithMessage("Title must at least 3");

            RuleFor(x => x.Request.Description)
                .MinimumLength(3).WithMessage("Description must at least 3");

            RuleFor(x => x.Request.QuestionType)
                .IsInEnum();

            When(x => x.Request.QuestionType is QuestionTypeDto.Radio or QuestionTypeDto.Checkbox, () =>
            {
                RuleFor(x => x.Request.Choices)
                    .NotNull()
                    .Must(c => c!.Count > 0)
                    .WithMessage("Choices required");
            });

            When(x => x.Request.QuestionType == QuestionTypeDto.Slider, () =>
            {
                RuleFor(x => x.Request.Config)
                    .NotNull()
                    .WithMessage("Slider Config required");
            });

            When(x => x.Request.QuestionType == QuestionTypeDto.Rating, () =>
            {
                RuleFor(x => x.Request.StarConfig)
                    .NotNull()
                    .WithMessage("Star config required");
            });

        }
    }
}
