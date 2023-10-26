using MediatR;

namespace Notepad.Application.Users.Command.Create
{
    public record CreateUserCommand() : IRequest<Guid>;
}
