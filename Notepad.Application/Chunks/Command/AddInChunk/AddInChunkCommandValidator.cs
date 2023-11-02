using FluentValidation;

namespace Notepad.Application.Chunks.Command.AddInChunk
{
    public class AddInChunkCommandValidator : AbstractValidator<AddInChunkCommand>
    {
        public AddInChunkCommandValidator()
        {
            RuleFor(x => x.ChunkId).NotNull().NotEqual(Guid.Empty);
            RuleFor(x => x.NoteId).NotNull().NotEqual(Guid.Empty);
        }
    }
}
