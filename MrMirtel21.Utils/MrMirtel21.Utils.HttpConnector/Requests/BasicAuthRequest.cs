using MrMirtel21.Utils.HttpConnector.Clients.BasuAuthentication;
using MrMirtel21.Utils.HttpConnector.Common;
using System.Net.Http.Headers;
using System.Text;

namespace MrMirtel21.Utils.HttpConnector.Requests
{
    public class BasicAuthRequest : ClientRequest
    {
        private readonly IBasicAuthenticationSettings _settings;
        private readonly HttpClient _httpClient;

        public BasicAuthRequest(IBasicAuthenticationSettings settings, HttpClient httpClient) : base(httpClient)
        {
            _settings = settings;
            _httpClient = httpClient;
        }

        protected override async Task<AuthToken> GetToken()
        {
            string usernamePassword = $"{_settings.Username}:{_settings.UserPassword}";
            var plainTextBytes = Encoding.UTF8.GetBytes(usernamePassword);
            var token = Convert.ToBase64String(plainTextBytes);

            return await Task.FromResult(new AuthToken { AccessToken = token });
        }

        protected override void SetToken(AuthToken token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", token.AccessToken);
        }
    }
}