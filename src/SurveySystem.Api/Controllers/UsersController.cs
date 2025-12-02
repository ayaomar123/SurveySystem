using MediatR;
using Microsoft.AspNetCore.Mvc;
using SurveySystem.Api.Requests.Users;
using SurveySystem.Application.Users.Commands.CreateUser;
using SurveySystem.Application.Users.Commands.UpdateUser;
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
            var command = new CreateUserCommand(
                request.Name,
                request.Email,
                request.PasswordHash,
                request.Role);

            var id = await mediator.Send(command);
            return Ok(new { Id = id, Message = "User created successfully." });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id,[FromBody] CreateUserRequest request)
        {
            var command = new UpdateUserCommand(
                id,
                request.Name,
                request.Email,
                request.PasswordHash,
                request.Role);

           await mediator.Send(command);

            return NoContent();
        }
    }
}
