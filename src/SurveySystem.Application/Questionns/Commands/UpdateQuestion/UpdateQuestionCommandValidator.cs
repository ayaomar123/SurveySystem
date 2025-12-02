using FluentValidation;
using SurveySystem.Application.Questions.Dtos;

namespace SurveySystem.Application.Questionns.Commands.UpdateQuestion
{
    public class UpdateQuestionCommandValidator : AbstractValidator<UpdateQuestionCommand>
    {
        public UpdateQuestionCommandValidator()
        {
            RuleFor(x =>  x.Request.Id)
                  .NotEmpty().WithMessage("Id is required");

            RuleFor(x =>  x.Request.Title)
                .NotEmpty().WithMessage("Title is required");

            RuleFor(x =>  x.Request.QuestionType)
                .IsInEnum().WithMessage("Invalid question type");

            When(x => 
            x.Request.QuestionType == QuestionTypeDto.Radio ||
            x.Request.QuestionType == QuestionTypeDto.Checkbox, () =>
                     {
                         RuleFor(x =>  x.Request.Choices)
                             .Must(c => c!.Count > 0).WithMessage("Choices required");
                     });

            When(x =>  x.Request.QuestionType == QuestionTypeDto.Slider, () =>
            {
                RuleFor(x =>  x.Request.SliderConfig)
                    .NotNull().WithMessage("SliderConfig required");
            });

            When(x =>  x.Request.QuestionType == QuestionTypeDto.Rating, () =>
            {
                RuleFor(x =>  x.Request.StarConfig)
                    .NotNull().WithMessage("StarConfig required");
            });
        }
    }
}
