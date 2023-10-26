using MediatR;
using Notepad.Domain.ViewModel;

namespace Notepad.Application.Chunks.Queries.GetUserChunks
{
    public record GetUserChunksQuery() : IRequest<IEnumerable<ChunkViewModel>>;
}
