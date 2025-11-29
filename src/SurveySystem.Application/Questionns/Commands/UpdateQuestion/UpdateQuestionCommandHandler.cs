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
        public async Task<QuestionResponseDto> Handle(UpdateQuestionCommand request, CancellationToken ct)
        {
            var question = await context.Questions
                 .Include(q => q.Choices)
                 .Include(q => q.SliderConfig)
                 .Include(q => q.StarConfig)
                 .FirstOrDefaultAsync(q => q.Id == request.Id, ct);

            if (question == null)
                throw new Exception("not found");

            question.UpdateBasicInfo(
               request.Title,
               request.Description,
               (QuestionType)request.QuestionType,
               request.IsRequired,
               request.Status
           );

            if (request.QuestionType == QuestionTypeDto.Radio || request.QuestionType == QuestionTypeDto.Checkbox)
            {
                if (request.Choices == null || !request.Choices.Any())
                    throw new ArgumentException("Choices cannot be empty for this question type.");

                question.UpdateChoices(
                    request.Choices.Select(c => new QuestionChoice(c.Text, c.Order)).ToList()
                );
            }

            else if (request.QuestionType == QuestionTypeDto.Slider)
            {
                question.UpdateSliderConfig(new SliderConfig(
                    request.SliderConfig!.Min,
                    request.SliderConfig.Max,
                    request.SliderConfig.Step,
                    request.SliderConfig.UnitLabel
                ));
            }

            else if (request.QuestionType == QuestionTypeDto.Rating)
            {
                question.UpdateStarConfig(new StarConfig(request.StarConfig!.MaxStar));
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
