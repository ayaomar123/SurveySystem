using MediatR;
using Microsoft.AspNetCore.Mvc;
using SurveySystem.Application.Users.Commands.CreateUser;
using SurveySystem.Application.Users.Dtos;
using SurveySystem.Application.Users.Queries.GetUsers;

namespace SurveySystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await mediator.Send(new GetUsersQuery());
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserRequest request)
        {
            var id = await mediator.Send(new CreateUserCommand(request));
            return Ok(new { Id = id, Message = "User created successfully." });
        }
    }
}
