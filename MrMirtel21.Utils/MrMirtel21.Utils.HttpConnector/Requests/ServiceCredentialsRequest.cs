using Newtonsoft.Json;
using MrMirtel21.Utils.HttpConnector.Clients.ServiceCredentials;
using MrMirtel21.Utils.HttpConnector.Common;
using System.Net.Http;
using System.Threading.Tasks;
using MrMirtel21.Utils.HttpConnector.Exceptions;

namespace MrMirtel21.Utils.HttpConnector.Requests
{
    public class ServiceCredentialsRequest : ClientRequest
    {
        private readonly ServiceCredentialsClient _client;

        public ServiceCredentialsRequest(HttpClient httpClient, ServiceCredentialsClient client) : base(httpClient)
        {
            _client = client;
        }

        protected override async Task<AuthToken> GetToken()
        {
            var response = await _client.GetAccessToken();
            var authToken = JsonConvert.DeserializeObject<AuthToken>(response);
            if (authToken == null)
            {
                throw new AuthTokenNotDeserializedException("Auth token not deserialized");
            }
            return authToken;
        }
    }
}