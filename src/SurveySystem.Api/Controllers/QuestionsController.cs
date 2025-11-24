using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveySystem.Application.Questionns.Commands.CreateQuestion;
using SurveySystem.Application.Questionns.Commands.UpdateQuestion;
using SurveySystem.Application.Questionns.Commands.UpdateQuestionStatus;
using SurveySystem.Application.Questionns.Dtos;
using SurveySystem.Application.Questionns.Queries.GetQuestions;

namespace SurveySystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QuestionsController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var questions = await mediator.Send(new GetQuestionsQuery());
            return Ok(questions);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateQuestionRequest request)
        {
            var id = await mediator.Send(new CreateQuestionCommand(request));
            return Ok(new { Id = id, Message = "Question created successfully." });
        }

        [HttpPut("{id}/edit")]
        public async Task<IActionResult> Edit(UpdateQuestionCommand command)
        {
            var result = await mediator.Send(command);

            return Ok(new { updated = true });
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(Guid id)
        {
            var result = await mediator.Send(new UpdateQuestionStatusCommand(id));

            return Ok(new { updated = true });
        }
    }
}
