using MediatR;

namespace Notepad.Application.Chunks.Command.Delete
{
    public record DeleteChunkCommand(Guid ChunkId) : IRequest<Guid>;
}
