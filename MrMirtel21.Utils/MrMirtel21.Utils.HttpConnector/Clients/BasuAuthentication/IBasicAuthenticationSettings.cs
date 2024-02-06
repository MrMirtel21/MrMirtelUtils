namespace MrMirtel21.Utils.HttpConnector.Clients.BasuAuthentication
{
    public interface IBasicAuthenticationSettings
    {
        string Username { get; set; }
        string UserPassword { get; set; }
    }
}