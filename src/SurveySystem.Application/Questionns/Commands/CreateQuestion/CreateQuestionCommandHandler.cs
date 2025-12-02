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
        public async Task<Guid> Handle(CreateQuestionCommand request,CancellationToken ct)
        {
            Question question;

            switch (request.Request.QuestionType)
            {
                case QuestionTypeDto.TextInput:
                    question = Question.CreateTextQuestion(
                                    request.Request.Title,
                                    request.Request.Description,
                                    request.Request.IsRequired
                                );
                    break;
                case QuestionTypeDto.YesOrNo:
                    question = Question.CreateYesNoQuestion(
                            request.Request.Title,
                            request.Request.Description,
                            request.Request.IsRequired
                        );
                    break;

                case QuestionTypeDto.Radio:
                    {
                        question = Question.CreateChoiceQuestion(
                            request.Request.Title,
                            QuestionType.Radio,
                            request.Request.Description,
                            request.Request.IsRequired,
                            request.Request.Choices!
                            .Select(c => new QuestionChoice(c.Text, c.Order)).ToList()
                        );
                        break;
                    }

                case QuestionTypeDto.Checkbox:
                    {
                        question = Question.CreateChoiceQuestion(
                            request.Request.Title,
                            QuestionType.Checkbox,
                            request.Request.Description,
                            request.Request.IsRequired,
                            request.Request.Choices!.Select(c => new QuestionChoice(c.Text, c.Order)).ToList()
                        );
                        break;
                    }

                case QuestionTypeDto.Slider:
                    if (request.Request.Config == null)
                        throw new ArgumentException("Config required");

                    question = Question.CreateSliderQuestion(
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
                    break;

                case QuestionTypeDto.Rating:
                    question = Question.CreateRatingQuestion(
                        request.Request.Title,
                        request.Request.Description,
                        request.Request.IsRequired,
                        new StarConfig(request.Request.StarConfig!.MaxStar)
                    );
                    break;

                default:
                    throw new Exception("Invalid Question Type");
            }

            await context.Questions.AddAsync(question, ct);
            await context.SaveChangesAsync(ct);

            return question.Id;
        }


    }
}
