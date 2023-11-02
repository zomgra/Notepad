namespace Notepad.Domain.Exceptions
{
    public class NotepadNotFoundException : Exception
    {
        public NotepadNotFoundException(string? message) : base(message)
        {
        }

        public NotepadNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

    }
}
