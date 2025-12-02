using FluentValidation;

namespace SurveySystem.Application.Questionns.Commands.UpdateQuestionStatus
{
    public sealed class UpdateQuestionStatusValidator : AbstractValidator<UpdateQuestionStatusCommand>
    {
        public UpdateQuestionStatusValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.");
        }
    }
}
