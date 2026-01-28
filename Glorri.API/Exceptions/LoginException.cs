namespace Glorri.API.Exceptions
{
    public class LoginException : Exception
    {
        public LoginException() : base("Password or username is not correct")
        {
        }
    }
}
