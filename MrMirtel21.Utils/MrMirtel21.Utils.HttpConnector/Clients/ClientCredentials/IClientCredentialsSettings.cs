namespace MrMirtel21.Utils.HttpConnector.Clients
{
    public interface IClientCredentialsSettings
    {
        string TokenUrl { get; set; }
        string ClientId { get; set; }
        string ClientSecret { get; set; }
    }
}