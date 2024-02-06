using MrMirtel21.Utils.HttpConnector.Common;

namespace MrMirtel21.Utils.HttpConnector.Requests
{
    public class ApiKeyClientRequest : ClientRequest
    {
        private readonly string ApiKey;

        public ApiKeyClientRequest(HttpClient httpClient, string apiKey) : base(httpClient)
        {
            ApiKey = apiKey;
        }

        protected override async Task<AuthToken> GetToken()
        {
            return await Task.FromResult(new AuthToken { AccessToken = ApiKey });
        }
    }
}