using MediatR;
using SurveySystem.Application.Interfaces;
using SurveySystem.Application.Questions.Dtos;
using SurveySystem.Domain.Entites.Questions;
using SurveySystem.Domain.Entites.Questions.Enums;

namespace SurveySystem.Application.Questionns.Commands.CreateQuestion
{
    public class CreateQuestionCommandHandler(IAppDbContext context)
        : IRequestHandler<CreateQuestionCommand, Guid>
    {


        public async Task<Guid> Handle(
            CreateQuestionCommand request,
            CancellationToken cancellationToken)
        {
            Question question = BuildQuestion(request);

            await context.Questions.AddAsync(question, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return question.Id;
        }

        private Question BuildQuestion(CreateQuestionCommand request)
        {
            if (request.Request.QuestionType == QuestionTypeDto.TextInput)
            {
                return Question.CreateTextQuestion(
                    request.Request.Title,
                    request.Request.Description,
                    request.Request.IsRequired
                );
            }

            else if (request.Request.QuestionType == QuestionTypeDto.YesOrNo)
            {
                return Question.CreateYesNoQuestion(
                    request.Request.Title,
                    request.Request.Description,
                    request.Request.IsRequired
                );
            }

            // Radio
            else if (request.Request.QuestionType == QuestionTypeDto.Radio)
            {
                if (request.Request.Choices == null || request.Request.Choices.Count == 0)
                    throw new ArgumentException("Choices are required for Radio questions.");

                return Question.CreateChoiceQuestion(
                    request.Request.Title,
                    QuestionType.Radio,
                    request.Request.Description,
                    request.Request.IsRequired,
                    request.Request.Choices!.Select(c => new QuestionChoice(c.Text, c.Order)).ToList()
                );
            }

            // Checkbox
            else if (request.Request.QuestionType == QuestionTypeDto.Checkbox)
            {
                if (request.Request.Choices == null || request.Request.Choices.Count == 0)
                    throw new ArgumentException("Choices are required for Checkbox questions.");

                return Question.CreateChoiceQuestion(
                    request.Request.Title,
                    QuestionType.Checkbox,
                    request.Request.Description,
                    request.Request.IsRequired,
                    request.Request.Choices!.Select(c => new QuestionChoice(c.Text, c.Order)).ToList()
                );
            }

            // Slider
            else if (request.Request.QuestionType == QuestionTypeDto.Slider)
            {
                if (request.Request.Config == null)
                    throw new ArgumentException("Config required");

                return Question.CreateSliderQuestion(
                    request.Request.Title,
                    request.Request.Description,
                    request.Request.IsRequired,
                    new SliderConfig(
                        request.Request.Config!.Min,
                        request.Request.Config.Max,
                        request.Request.Config.Step,
                        request.Request.Config.UnitLabel
                    )
                );
            }

            // Rating (Stars)
            else if (request.Request.QuestionType == QuestionTypeDto.Rating)
            {
                if (request.Request.StarConfig == null)
                    throw new ArgumentException("StarConfig required");

                return Question.CreateRatingQuestion(
                    request.Request.Title,
                    request.Request.Description,
                    request.Request.IsRequired,
                    new StarConfig(request.Request.StarConfig!.MaxStar)
                );
            }

            // Invalid type
            else
            {
                throw new ArgumentOutOfRangeException(nameof(request.Request.QuestionType), "Invalid Question Type");
            }
        }

    }
}
