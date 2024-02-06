using System.Text;

namespace MrMirtel21.Utils.HttpConnector.Clients
{
    public class ClientCredentialsClient
    {
        public string ClientId { get; private set; }
        public string ClientSecret { get; private set; }
        public string TokenUrl { get; private set; }


        public ClientCredentialsClient(string clientId, string clientSecret, string tokenUrl)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
            TokenUrl = tokenUrl;
        }

        private HttpContent GetContent()
        {
            string body = $"client_id={ClientId}" +
                          $"&client_secret={ClientSecret}" +
                          $"&grant_type=client_credentials";

            return new StringContent(body, Encoding.UTF8, "application/x-www-form-urlencoded");
        }

        public async Task<HttpResponseMessage> RequestAuthToken()
        {
            HttpResponseMessage response;
            using (var httpClient = new HttpClient())
            {
                string url = TokenUrl;
                HttpContent content = GetContent();
                response = await httpClient.PostAsync(url, content);
            }
            return response;
        }
    }
}