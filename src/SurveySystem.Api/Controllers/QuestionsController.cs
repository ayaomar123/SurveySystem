using MediatR;
using Microsoft.AspNetCore.Mvc;
using SurveySystem.Application.Questionns.Commands.CreateQuestion;
using SurveySystem.Application.Questionns.Dtos;

namespace SurveySystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create(CreateQuestionRequest request)
        {
            var id = await mediator.Send(new CreateQuestionCommand(request));
            return Ok(new { Id = id, Message = "Question created successfully." });
        }
    }
}
