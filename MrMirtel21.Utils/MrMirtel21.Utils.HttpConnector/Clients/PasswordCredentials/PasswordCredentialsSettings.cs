namespace MrMirtel21.Utils.HttpConnector.Clients
{
    public class PasswordCredentialsSettings : IPasswordCredentialsSettings
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Username { get; set; }
        public string UserPassword { get; set; }
        public string TokenUrl { get; set; }
    }
}
