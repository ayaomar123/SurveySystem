using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Application.Interfaces;
using SurveySystem.Application.Questionns.Dtos;
using SurveySystem.Application.Questions.Dtos;
using SurveySystem.Domain.Entites.Questions;
using SurveySystem.Domain.Entites.Questions.Enums;

namespace SurveySystem.Application.Questionns.Commands.UpdateQuestion
{
    public class UpdateQuestionCommandHandler(IAppDbContext context) : IRequestHandler<UpdateQuestionCommand, QuestionResponseDto>
    {
        public async Task<QuestionResponseDto> Handle(UpdateQuestionCommand command, CancellationToken ct)
        {
            var question = await context.Questions
                 .Include(q => q.Choices)
                 .Include(q => q.SliderConfig)
                 .Include(q => q.StarConfig)
                 .FirstOrDefaultAsync(q => q.Id == command.Request.Id, ct);

            if (question == null)
                throw new Exception("not found");

            question.UpdateBasicInfo(
               command.Request.Title,
               command.Request.Description,
               (QuestionType)command.Request.QuestionType,
               command.Request.IsRequired,
               command.Request.Status
           );

            switch (command.Request.QuestionType)
            {
                case QuestionTypeDto.Radio:
                case QuestionTypeDto.Checkbox:
                    {
                        question.UpdateChoices(
                            command.Request!.Choices!.Select(c => new QuestionChoice(c.Text, c.Order)).ToList()
                        );
                        break;
                    }

                case QuestionTypeDto.Slider:
                    question.UpdateSliderConfig(new SliderConfig(
                                    command.Request.SliderConfig!.Min,
                                    command.Request.SliderConfig.Max,
                                    command.Request.SliderConfig.Step,
                                    command.Request.SliderConfig.UnitLabel
                                ));
                    break;
                case QuestionTypeDto.Rating:
                    question.UpdateStarConfig(new StarConfig(command.Request.StarConfig!.MaxStar));
                    break;
            }

            await context.SaveChangesAsync(ct);

            var response = new QuestionResponseDto(
                question.Id,
                question.Title,
                question.Description,
                (int)question.QuestionType,
                question.IsRequired,
                question.Status,
                question.Choices,
                question.SliderConfig,
                question.StarConfig
            );

            return response;
    }
    }
}
