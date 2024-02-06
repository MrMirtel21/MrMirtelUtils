using MrMirtel21.Utils.HttpConnector.Clients;
using MrMirtel21.Utils.HttpConnector.Common;
using MrMirtel21.Utils.HttpConnector.Exceptions;
using Newtonsoft.Json;

namespace MrMirtel21.Utils.HttpConnector.Requests
{
    public class ClientCredentialsClientRequest : ClientRequest
    {
        private readonly ClientCredentialsClient _client;

        public ClientCredentialsClientRequest(HttpClient httpClient, ClientCredentialsClient client) : base(httpClient)
        {
            _client = client;
        }

        protected override async Task<AuthToken> GetToken()
        {
            AuthToken? authToken = null;
            HttpResponseMessage response = await _client.RequestAuthToken();
            if (!response.IsSuccessStatusCode)
            {
                throw new AuthTokenNotReceivedException("Auth token not found.");
            }

            string responseContent = await response.Content.ReadAsStringAsync();
            authToken = JsonConvert.DeserializeObject<AuthToken>(responseContent);

            if(authToken == null) 
            {
                throw new AuthTokenNotDeserializedException("Auth token not deserialized");
            }
            return authToken!;

        }
    }
}