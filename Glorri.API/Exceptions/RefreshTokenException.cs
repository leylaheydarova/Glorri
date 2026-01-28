namespace Glorri.API.Exceptions
{
    public class RefreshTokenException : Exception
    {
        public RefreshTokenException() : base("Invalid or expired refresh token")
        {
        }
    }
}
