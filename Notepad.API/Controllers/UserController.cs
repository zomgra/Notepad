using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notepad.API.Schemes;
using Notepad.Application.Users.Command.Create;
using Notepad.Storage;
using System.Threading.Tasks;

namespace Notepad.API.Controllers
{
    [ApiController, Route("api/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = nameof(CookieHaveUIDScheme))]

    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser()
        {
            var userId = await _mediator.Send(new CreateUserCommand());
            return Ok(userId);
        }

        /// <summary>
        /// Delete in future
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> DeleteAllUsers([FromServices] NoteDbContext context)
        {
            var userCount = context.Users.Count();
            context.RemoveRange(context.Users.ToList());
            context.SaveChanges();
            return Ok(userCount);
        }
    }
}
