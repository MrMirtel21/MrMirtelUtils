namespace MrMirtel21.Utils.HttpConnector.Exceptions
{
    public class AuthTokenNotReceivedException : Exception
    {
        public AuthTokenNotReceivedException(string? message) : base(message)
        {
        }
    }
}
