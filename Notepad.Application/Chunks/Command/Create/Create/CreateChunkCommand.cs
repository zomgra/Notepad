using MediatR;
using Notepad.Domain.ViewModel;

namespace Notepad.Application.Chunks.Command.Create.Create
{
    public record CreateChunkCommand(string name) : IRequest<ChunkViewModel>;
}
