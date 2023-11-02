using FluentValidation;

namespace Notepad.Application.Notes.Command.Create
{
    public class CreateNoteCommandValidator : AbstractValidator<CreateNoteCommand>
    {
        public CreateNoteCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(128)
                .MinimumLength(3)
                .WithMessage("Title length 3 - 128");

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(1000)
                .MinimumLength(10)
                .WithMessage("Description length 10 - 1000");
        }
    }
}
