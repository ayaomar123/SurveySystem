using FluentValidation;

namespace SurveySystem.Application.Surveys.Commands.UpdateSurveyStatus
{
    public sealed class UpdateSurveyStatusCommandValidator
        : AbstractValidator<UpdateSurveyStatusCommand>
    {
        public UpdateSurveyStatusCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Survey Id is required.");

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Invalid survey status.");
        }
    }
}
