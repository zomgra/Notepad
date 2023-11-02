using MediatR;

namespace Notepad.Application.Chunks.Command.Rename
{
    public record RenameChunkCommand(Guid ChunkId, string NewName) : IRequest<Unit>;
}
