using Microsoft.AspNetCore.Http;
using MrMirtel21.Utils.HttpConnector.Clients;
using MrMirtel21.Utils.HttpConnector.Clients.BasuAuthentication;
using MrMirtel21.Utils.HttpConnector.Clients.ServiceCredentials;
using MrMirtel21.Utils.HttpConnector.Requests;

namespace MrMirtel21.Utils.HttpConnector
{
    public interface IClientRequestFactory
    {
        IClientRequest ClientCredentials(IClientCredentialsSettings settings, HttpClient httpClient);
        IClientRequest PassThrough(IHttpContextAccessor httpContextAccessor, HttpClient httpClient);
        IClientRequest PasswordCredentials(IPasswordCredentialsSettings settings, HttpClient httpClient);
        IClientRequest ApiKeyCredentials(IHttpContextAccessor httpContextAccessor, HttpClient httpClient, string apiKey);
        IClientRequest NoAuth(IHttpContextAccessor httpContextAccessor, HttpClient httpClient);
        IClientRequest BasicAuth(IBasicAuthenticationSettings settings, HttpClient httpClient);
        IClientRequest ServiceCredentials(ServiceCredentialsClient client, HttpClient httpClient);

    }
}