namespace Glorri.API.Exceptions
{
    public class NotFoundException : Exception
    {

        public NotFoundException(string message) : base($"Sorry, {message} is not found!")
        {
        }
    }
}
