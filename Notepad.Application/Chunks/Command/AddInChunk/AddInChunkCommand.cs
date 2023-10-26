using MediatR;

namespace Notepad.Application.Chunks.Command.AddInChunk
{
    public record AddInChunkCommand(Guid noteId, Guid chunkId) : IRequest<bool>;
}
