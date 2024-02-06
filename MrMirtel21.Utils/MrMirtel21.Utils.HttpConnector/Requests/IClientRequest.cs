namespace MrMirtel21.Utils.HttpConnector.Requests
{
    public interface IClientRequest
    {
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage message);
    }
}
