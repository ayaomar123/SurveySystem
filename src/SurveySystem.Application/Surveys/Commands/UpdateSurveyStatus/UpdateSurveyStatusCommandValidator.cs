using FluentValidation;
using SurveySystem.Domain.Entites.Surveys.Enums;

namespace SurveySystem.Application.Surveys.Commands.UpdateSurveyStatus
{
    public sealed class UpdateSurveyStatusCommandValidator
        : AbstractValidator<UpdateSurveyStatusCommand>
    {
        public UpdateSurveyStatusCommandValidator()
        {
            RuleFor(x => x.Request.Id)
                .NotEmpty().WithMessage("Survey Id is required.");

            RuleFor(x => x.Request.Status)
                .IsInEnum().WithMessage("Invalid survey status.");

            When(x => x.Request.Status == SurveyStatus.Active, () =>
            {
                RuleFor(x => x.Request.StartDate)
                    .NotNull().WithMessage("StartDate is required when Status Active.")
                    .Must(start => start > DateTime.Now)
                    .WithMessage("StartDate > now"); ;

                RuleFor(x => x.Request.EndDate)
                    .NotNull().WithMessage("EndDate is required when Status Active.");

                RuleFor(x => x)
                    .Must(x => x.Request.StartDate <= x.Request.EndDate)
                    .WithMessage("StartDate must be earlier than EndDate.");
            });
        }
    }
}
