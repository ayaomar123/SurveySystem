using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveySystem.Api.Requests.Surveys;
using SurveySystem.Api.Requests.Surveys.Responses;
using SurveySystem.Application.Surveys.Commands.CreateSurvey;
using SurveySystem.Application.Surveys.Commands.CreateSurvey.Dtos;
using SurveySystem.Application.Surveys.Commands.UpdateSurvey;
using SurveySystem.Application.Surveys.Commands.UpdateSurvey.Dtos;
using SurveySystem.Application.Surveys.Commands.UpdateSurveyStatus;
using SurveySystem.Application.Surveys.Commands.UpdateSurveyStatus.Dtos;
using SurveySystem.Application.Surveys.Queries.GetSurveyById;
using SurveySystem.Application.Surveys.Queries.GetSurveys;
using SurveySystem.Application.Surveys.Queries.GetSurveys.Dtos;
using SurveySystem.Application.Surveys.Responses.Commands.SubmitSurvey;
using SurveySystem.Application.Surveys.Responses.Commands.SubmitSurvey.Dtos;
using SurveySystem.Application.Surveys.Responses.Queries.GetSurveyResponse;

namespace SurveySystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class SurveysController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]GetSurveyQueryRequest request)
        {
            var dto = new GetSurveyQueryDto(request.title,request.status);
            var questions = await mediator.Send(new GetSurveyQuery(dto));
            return Ok(questions);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Show(Guid id)
        {
            var questions = await mediator.Send(new GetSurveyByIdQuery(id));
            return Ok(questions);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSurvey
            ([FromBody] CreateSurveyRequest request)
        {
            var dto = new CreateSurveyDto(
                request.Title,
                request.Description,
                request.Status,
                request.StartDate,
                request.EndDate,
                request.Questions.Select(q =>
                    new SurveyQuestionItem(q.QuestionId, q.Order)).ToList());
            
            var command = new CreateSurveyCommand(dto);

            var surveyId = await mediator.Send(command);

            return Ok(new { SurveyId = surveyId });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSurvey(Guid id, [FromBody] UpdateSurveyRequest request)
        {
            var dto = new UpdateSurveyDto(id,
                request.Title,
                request.Description,
                request.Status,
                request.StartDate,
                request.EndDate,
                request.Questions.Select(q =>
                    new SurveyQuestionItem(q.QuestionId, q.Order)).ToList());

            var command = new UpdateSurveyCommand(dto);

            var updatedSurvey = await mediator.Send(command);
            return NoContent();
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateSurveyStatus(Guid id, [FromBody] UpdateSurveyStatusRequest request)
        {
            var dto = new UpdateSurveyStatusDto(
                id,
                request.Status,
                request.StartDate,
                request.EndDate);

            var command = new UpdateSurveyStatusCommand(dto);

            var updatedSurvey = await mediator.Send(command);
            return NoContent();
        }

        [HttpPost("{surveyId}/submit")]
        [AllowAnonymous]
        public async Task<IActionResult> SubmitSurveyResponse(
            Guid surveyId,
            SubmitSurveyRequest request)
        {
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString();

            var userAgent = Request.Headers["User-Agent"].ToString();

            var dto = new SubmitSurveyCommandDto(
                surveyId,
                ip,
                userAgent,
                request.Answers.Select(a => new SubmitSurveyAnswerDto
                {
                    QuestionId = a.QuestionId,
                    Value = a.Value,
                    SelectedChoiceId = a.SelectedChoiceId,
                    SelectedChoicesIds = a.SelectedChoices
                }).ToList());

            var command = new SubmitSurveyCommand(dto);

            var responseId = await mediator.Send(command);

            return Ok(new { responseId });
        }

        [HttpGet("{surveyId}/analytics")]
        public async Task<IActionResult> GetAnalytics(Guid surveyId)
        {
            var result = await mediator.Send(new GetSurveyAnalyticsQuery(surveyId));
            return Ok(result);
        }
    }
}
