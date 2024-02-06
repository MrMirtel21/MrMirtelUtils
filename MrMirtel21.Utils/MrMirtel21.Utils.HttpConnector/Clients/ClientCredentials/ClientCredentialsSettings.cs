namespace MrMirtel21.Utils.HttpConnector.Clients
{
    public class ClientCredentialsSettings : IClientCredentialsSettings
    {
        public string TokenUrl { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}