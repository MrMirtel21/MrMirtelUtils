using MrMirtel21.Utils.HttpConnector.Common;
using System.Net;
using System.Net.Http.Headers;

namespace MrMirtel21.Utils.HttpConnector.Requests
{
    public abstract class ClientRequest : IClientRequest
    {
        private readonly HttpClient _httpClient;

        protected ClientRequest(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage message)
        {
            if (!_httpClient.DefaultRequestHeaders.Contains("Authorization"))
            {
                SetToken(await GetToken());
            }

            var response = await _httpClient.SendAsync(message).ConfigureAwait(false);
            if (response.StatusCode != HttpStatusCode.Unauthorized) return response;

            SetToken(await GetToken());
            HttpRequestMessage clonedMessage = await CloneHttpRequestMessageAsync(message);
            response = await _httpClient.SendAsync(clonedMessage).ConfigureAwait(false);

            return response;
        }

        protected abstract Task<AuthToken> GetToken();

        protected virtual void SetToken(AuthToken token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
        }

        private static async Task<HttpRequestMessage> CloneHttpRequestMessageAsync(HttpRequestMessage request)
        {
            HttpRequestMessage clone = new HttpRequestMessage(request.Method, request.RequestUri);
            var ms = new MemoryStream();
            if (request.Content != null)
            {
                await request.Content.CopyToAsync(ms).ConfigureAwait(false);
                ms.Position = 0;
                clone.Content = new StreamContent(ms);

                if (request.Content.Headers != null)
                    foreach (var h in request.Content.Headers)
                        clone.Content.Headers.Add(h.Key, h.Value);
            }


            clone.Version = request.Version;

            foreach (KeyValuePair<string, object?> option in request.Options)
                clone.Options!.Append(option);

            foreach (KeyValuePair<string, IEnumerable<string>> header in request.Headers)
                clone.Headers.TryAddWithoutValidation(header.Key, header.Value);

            clone.Headers.Authorization = null;

            return clone;
        }
    }
}
