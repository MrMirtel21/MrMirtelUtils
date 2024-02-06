namespace MrMirtel21.Utils.HttpConnector.Clients
{
    public interface IPasswordCredentialsSettings
    {
        string ClientId { get; }
        string ClientSecret { get; }
        string TokenUrl { get; }
        string Username { get; set; }
        string UserPassword { get; set; }
    }
}