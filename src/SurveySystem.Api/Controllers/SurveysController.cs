using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveySystem.Api.Requests.Surveys;
using SurveySystem.Application.Surveys.Commands.CreateSurvey;
using SurveySystem.Application.Surveys.Commands.UpdateSurvey;
using SurveySystem.Application.Surveys.Commands.UpdateSurveyStatus;
using SurveySystem.Application.Surveys.Queries.GetSurveyById;
using SurveySystem.Application.Surveys.Queries.GetSurveys;
using SurveySystem.Application.Surveys.Responses.Commands.SubmitSurveyResponse;
using SurveySystem.Application.Surveys.Responses.Dtos;
using SurveySystem.Application.Surveys.Responses.Queries.GetSurveyResponse;

namespace SurveySystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SurveysController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var questions = await mediator.Send(new GetSurveyQuery());
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
        public async Task<IActionResult> CreateSurvey([FromBody] CreateSurveyRequest request)
        {
            var command = new CreateSurveyCommand(
                request.Title,
                request.Description,
                request.Status,
                request.StartDate,
                request.EndDate,
                request.Questions.Select(q =>
                    new SurveyQuestionItem(q.QuestionId, q.Order)).ToList()
            );

            var surveyId = await mediator.Send(command);

            return Ok(new { SurveyId = surveyId });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSurvey(Guid id, [FromBody] UpdateSurveyRequest request)
        {
            var command = new UpdateSurveyCommand(
                id,
                request.Title,
                request.Description,
                request.Status,
                request.StartDate,
                request.EndDate,
                request.Questions.Select(q =>
                    new SurveyQuestionItem(q.QuestionId, q.Order)).ToList()
            );
            var updatedSurvey = await mediator.Send(command);
            return NoContent();
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateSurveyStatus(Guid id, [FromBody] UpdateSurveyStatusRequest request)
        {
            var command = new UpdateSurveyStatusCommand(
                id,
                request.Status,
                request.StartDate,
                request.EndDate
            );
            var updatedSurvey = await mediator.Send(command);
            return NoContent();
        }

        [HttpPost("{surveyId}/submit")]
        [AllowAnonymous]
        public async Task<IActionResult> SubmitSurveyResponse(
            Guid surveyId,
            SubmitSurveyResponseRequest request)
        {
            var ip = Request.Headers["X-Forwarded-For"].FirstOrDefault()
             ?? HttpContext.Connection.RemoteIpAddress?.ToString();

            var userAgent = Request.Headers["User-Agent"].ToString();

            var command = new SubmitSurveyResponseCommand
            {

                SurveyId = surveyId,
                IpAddress = ip,
                UserAgent = userAgent,
                Answers = request.Answers.Select(a => new SubmitAnswerDto
                {
                    QuestionId = a.QuestionId,
                    Value = a.Value,
                    SelectedChoiceId = a.SelectedChoiceId,
                    SelectedChoicesIds = a.SelectedChoices
                }).ToList()
            };

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
