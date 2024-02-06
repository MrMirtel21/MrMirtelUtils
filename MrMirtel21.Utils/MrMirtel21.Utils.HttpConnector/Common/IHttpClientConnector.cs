using Newtonsoft.Json;

namespace MrMirtel21.Utils.HttpConnector.Common
{
    public interface IHttpClientConnector
    {
        Task<TResponse?> PostAsync<TRequest, TResponse>(string url, TRequest body,
            JsonSerializerSettings? requestSerializerSettings = null,
            JsonSerializerSettings? responseSerializerSettings = null,
            bool acceptError = false);

        Task<TResponse?> PostAsync<TResponse>(string url, IEnumerable<KeyValuePair<string, string>> body,
            JsonSerializerSettings? responseSerializerSettings = null, bool acceptError = false);

        Task<TResponse?> GetAsync<TResponse>(string url,
            JsonSerializerSettings? responseSerializerSettings = null);

        Task<TResponse?> PutAsync<TRequest, TResponse>(string url, TRequest body,
            JsonSerializerSettings? requestSerializerSettings = null,
            JsonSerializerSettings? responseSerializerSettings = null);

        Task<TResponse?> PatchAsync<TRequest, TResponse>(string url, TRequest body,
            JsonSerializerSettings? requestSerializerSettings = null,
            JsonSerializerSettings? responseSerializerSettings = null);

        Task DeleteAsync(string url);
    }
}