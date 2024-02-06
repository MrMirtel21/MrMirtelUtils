using MrMirtel21.Utils.HttpConnector.Connections;
using MrMirtel21.Utils.HttpConnector.Requests;
using Newtonsoft.Json;
using System.Text;

namespace MrMirtel21.Utils.HttpConnector.Common
{
    public class HttpClientConnector : IHttpClientConnector
    {
        private readonly IConnectionBehavior _connection;
        private readonly IClientRequest _clientRequest;

        public HttpClientConnector(IConnectionBehavior connection, IClientRequest clientRequest)
        {
            _connection = connection;
            _clientRequest = clientRequest;
        }

        public async Task<TResponse?> PostAsync<TRequest, TResponse>(string url, TRequest body,
            JsonSerializerSettings? requestSerializerSettings = null,
            JsonSerializerSettings? responseSerializerSettings = null,
            bool acceptError = false)
        {
            return await _connection!.ExecuteRequest(async () =>
            {
                var resolvedRequestSerializer = requestSerializerSettings ?? new JsonSerializerSettings();
                var jsonBody = JsonConvert.SerializeObject(body, resolvedRequestSerializer);
                var message = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = new StringContent(jsonBody, Encoding.UTF8, "application/json")
                };
                var resolvedResponseSerializer = responseSerializerSettings ?? new JsonSerializerSettings();
                return await SendAndConvert<TResponse>(_clientRequest, message,
                    resolvedResponseSerializer, acceptError);
            });
        }

        /// <summary>
        /// Makes an POST HTTP request to the specified URL with x-www-form-urlencoded content-type 
        /// </summary>
        /// <typeparam name="TResponse">Type of content response</typeparam>
        /// <param name="url">URL of the request</param>
        /// <param name="body">Content of the request as lists of keyvalue pairs</param>
        /// <param name="responseSerializerSettings"></param>
        /// <param name="acceptError"></param>
        /// <returns></returns>
        public async Task<TResponse?> PostAsync<TResponse>(string url, IEnumerable<KeyValuePair<string, string>> body,
            JsonSerializerSettings? responseSerializerSettings = null, bool acceptError = false)
        {
            return await _connection.ExecuteRequest(async () =>
            {
                using var content = new FormUrlEncodedContent(body);
                content.Headers.Clear();
                content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                var message = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = content
                };
                return await SendAndConvert<TResponse>(_clientRequest, message,
                responseSerializerSettings ?? new JsonSerializerSettings(), acceptError);
            });
        }

        public async Task<TResponse?> GetAsync<TResponse>(string url,
            JsonSerializerSettings? responseSerializerSettings = null)
        {
            return await _connection.ExecuteRequest(async () =>
            {
                var message = new HttpRequestMessage(HttpMethod.Get, url);
                return await SendAndConvert<TResponse>(_clientRequest, message,
                    responseSerializerSettings ?? new JsonSerializerSettings());
            });
        }

        public async Task<TResponse?> PutAsync<TRequest, TResponse>(string url, TRequest body,
            JsonSerializerSettings? requestSerializerSettings = null,
            JsonSerializerSettings? responseSerializerSettings = null)
        {
            return await _connection.ExecuteRequest(async () =>
            {
                var jsonBody = JsonConvert.SerializeObject(body, requestSerializerSettings ?? new JsonSerializerSettings());
                var message = new HttpRequestMessage(HttpMethod.Put, url)
                {
                    Content = new StringContent(jsonBody, Encoding.UTF8, "application/json")
                };
                return await SendAndConvert<TResponse>(_clientRequest, message,
                    responseSerializerSettings ?? new JsonSerializerSettings());
            });
        }

        public async Task<TResponse?> PatchAsync<TRequest, TResponse>(string url, TRequest body,
            JsonSerializerSettings? requestSerializerSettings = null,
            JsonSerializerSettings? responseSerializerSettings = null)
        {
            return await _connection.ExecuteRequest(async () =>
            {
                var jsonBody = JsonConvert.SerializeObject(body, requestSerializerSettings ?? new JsonSerializerSettings());
                var message = new HttpRequestMessage(HttpMethod.Patch, url)
                {
                    Content = new StringContent(jsonBody, Encoding.UTF8, "application/json")
                };
                return await SendAndConvert<TResponse>(_clientRequest, message,
                    responseSerializerSettings ?? new JsonSerializerSettings());
            });
        }

        public async Task DeleteAsync(string url)
        {
            await _connection.ExecuteRequest(async () =>
            {
                var message = new HttpRequestMessage(HttpMethod.Delete, url);
                await SendAndConvert<EmptyResponse>(_clientRequest, message, null);
            });
        }

        private async Task<TResponse?> SendAndConvert<TResponse>(IClientRequest request, HttpRequestMessage message,
            JsonSerializerSettings? serializerSettings, bool acceptError = false)
        {
            var response = await request.SendAsync(message);
            if (!acceptError && !response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException(response.ReasonPhrase);
            }

            if (typeof(TResponse) == typeof(EmptyResponse))
            {
                return default;
            }

            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponse>(content, serializerSettings);
        }
    }
}