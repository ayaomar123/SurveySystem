using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveySystem.Api.Requests.Questions;
using SurveySystem.Application.Questionns.Commands.CreateQuestion;
using SurveySystem.Application.Questionns.Commands.CreateQuestion.Dtos;
using SurveySystem.Application.Questionns.Commands.UpdateQuestion;
using SurveySystem.Application.Questionns.Commands.UpdateQuestion.Dtos;
using SurveySystem.Application.Questionns.Commands.UpdateQuestionStatus;
using SurveySystem.Application.Questionns.Queries.GetQuestions;

namespace SurveySystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "Admin")]
    public class QuestionsController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? title, [FromQuery] bool? status)
        {
            var questions = await mediator.Send(new GetQuestionsQuery(title, status));
            return Ok(questions);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateQuestionRequest request)
        {
            var req = new CreateQuestionDto(
               request.Title,
               request.Description,
               request.QuestionType,
               request.IsRequired,
               request.Status,
               request.Choices,
               request.Config,
               request.StarConfig);

            var command = new CreateQuestionCommand(req);
            var id = await mediator.Send(command);
            return Ok(new { Id = id, Message = "Question created successfully." });
        }

        [HttpPut("{id}/edit")]
        public async Task<IActionResult> Edit(Guid id,[FromBody] UpdateQuestionRequest request)
        {
            var req = new UpdateQuestionDto(
                id,
                request.Title,
                request.Description,
                request.QuestionType,
                request.IsRequired,
                request.Status,
                request.Choices,
                request.Config,
                request.StarConfig);

            var command = new UpdateQuestionCommand(req);
            var updatedSurvey = await mediator.Send(command);
            return NoContent();
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(Guid id)
        {
            await mediator.Send(new UpdateQuestionStatusCommand(id));

            return NoContent();
        }
    }
}
