using MediatR;

namespace Notepad.Application.Chunks.Command.AddInChunk
{
    public record AddInChunkCommand(Guid NoteId, Guid ChunkId) : IRequest<bool>;
}
