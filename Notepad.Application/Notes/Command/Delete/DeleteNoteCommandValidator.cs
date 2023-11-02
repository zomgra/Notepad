using FluentValidation;

namespace Notepad.Application.Notes.Command.Delete
{
    public class DeleteNoteCommandValidator : AbstractValidator<DeleteNoteCommand>
    {
        public DeleteNoteCommandValidator()
        {
            RuleFor(c=>c.NoteId).NotNull().NotEmpty().NotEqual(Guid.Empty);
        }
    }
}
