using FluentValidation;
using SurveySystem.Domain.Entites.Surveys.Enums;

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

            When(x => x.Status == SurveyStatus.Active, () =>
            {
                RuleFor(x => x.StartDate)
                    .NotNull().WithMessage("StartDate is required when Status Active.")
                    .Must(start => start > DateTime.Now)
                    .WithMessage("StartDate > now"); ;

                RuleFor(x => x.EndDate)
                    .NotNull().WithMessage("EndDate is required when Status Active.");

                RuleFor(x => x)
                    .Must(x => x.StartDate <= x.EndDate)
                    .WithMessage("StartDate must be earlier than EndDate.");
            });
        }
    }
}
