using FluentValidation;
using SurveySystem.Application.Questions.Dtos;

namespace SurveySystem.Application.Questionns.Commands.UpdateQuestion
{
    public class UpdateQuestionCommandValidator : AbstractValidator<UpdateQuestionCommand>
    {
        public UpdateQuestionCommandValidator()
        {
            RuleFor(x => x.Id)
                  .NotEmpty().WithMessage("Question ID is required.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.");

            RuleFor(x => x.QuestionType)
                .IsInEnum().WithMessage("Invalid question type.");

            When(x => x.QuestionType == QuestionTypeDto.Radio ||
                     x.QuestionType == QuestionTypeDto.Checkbox, () =>
                     {
                         RuleFor(x => x.Choices)
                             .NotNull().WithMessage("Choices are required.")
                             .Must(c => c!.Count > 0).WithMessage("Choices cannot be empty.");
                     });

            When(x => x.QuestionType == QuestionTypeDto.Slider, () =>
            {
                RuleFor(x => x.SliderConfig)
                    .NotNull().WithMessage("SliderConfig is required.");
            });

            When(x => x.QuestionType == QuestionTypeDto.Rating, () =>
            {
                RuleFor(x => x.StarConfig)
                    .NotNull().WithMessage("StarConfig is required.");
            });
        }
    }
}
