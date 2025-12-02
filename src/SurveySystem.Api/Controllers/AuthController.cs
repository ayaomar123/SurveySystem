using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveySystem.Api.Requests;
using SurveySystem.Application.Statics.Queries;
using SurveySystem.Application.Users.Commands.Login;

namespace SurveySystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthRequest request)
        {
            var result = await mediator.Send(new LoginCommand(request.Email, request.Password));
            return Ok(result);
        }

        [HttpGet("statics")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> getStatics()
        {
            var result = await mediator.Send(new GetStaticQuery());
            return Ok(result);
        }
    }
}
