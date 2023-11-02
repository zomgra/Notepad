using FluentValidation;

namespace Notepad.Application.Chunks.Command.Create.Create
{
    public class CreateChunkCommandValidator : AbstractValidator<CreateChunkCommand>
    {
        public CreateChunkCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty()
                .MinimumLength(3)
                .MaximumLength(128)
                .WithMessage("Name length must be 3 - 128 chars");
        }
    }
}
