using System.Text;

namespace MrMirtel21.Utils.HttpConnector.Clients
{
    internal class PasswordCredentialsClient
    {
        public string ClientId { get; private set; }
        public string ClientSecret { get; private set; }
        public string Username { get; set; }
        public string UserPassword { get; set; }
        public string TokenUrl { get; private set; }


        public PasswordCredentialsClient(string clientId, string clientSecret, string username, string userPassword, string tokenUrl)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
            Username = username;
            UserPassword = userPassword;
            TokenUrl = tokenUrl;
        }

        private HttpContent GetContent()
        {
            string body = $"username={Username}" +
                          $"&password={UserPassword}" +
                          $"&client_id={ClientId}" +
                          $"&grant_type=password";

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
