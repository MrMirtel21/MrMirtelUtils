namespace MrMirtel21.Utils.HttpConnectorClients.BasuAuthentication
{
    public class BasicAuthenticationSettings : IBasicAuthenticationSettings
    {
        public string Username { get; set; }
        public string UserPassword { get; set; }
    }
}