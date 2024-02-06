using Microsoft.AspNetCore.Http;
using MrMirtel21.Utils.HttpConnector.Clients;
using MrMirtel21.Utils.HttpConnector.Clients.BasuAuthentication;
using MrMirtel21.Utils.HttpConnector.Clients.ServiceCredentials;
using MrMirtel21.Utils.HttpConnector.Requests;

namespace MrMirtel21.Utils.HttpConnector.Factories
{
    public class ClientRequestFactory : IClientRequestFactory
    {
        public IClientRequest ClientCredentials(IClientCredentialsSettings settings, HttpClient httpClient)
        {
            return new ClientCredentialsClientRequest(httpClient,
                new ClientCredentialsClient(settings.ClientId, settings.ClientSecret, settings.TokenUrl));
        }

        public IClientRequest PassThrough(IHttpContextAccessor httpContextAccessor, HttpClient httpClient)
        {
            return new PassThroughClientRequest(httpClient, httpContextAccessor);
        }

        public IClientRequest PasswordCredentials(IPasswordCredentialsSettings settings, HttpClient httpClient)
        {
            return new PasswordCredentialsRequest(httpClient, new PasswordCredentialsClient(settings.ClientId, settings.ClientSecret, settings.Username, settings.UserPassword, settings.TokenUrl));
        }

        public IClientRequest ApiKeyCredentials(IHttpContextAccessor httpContextAccessor, HttpClient httpClient, string apiKey)
        {
            return new ApiKeyClientRequest(httpClient, apiKey);
        }

        public IClientRequest NoAuth(IHttpContextAccessor httpContextAccessor, HttpClient httpClient)
        {
            return new NoAuthRequest(httpClient, httpContextAccessor);
        }

        public IClientRequest BasicAuth(IBasicAuthenticationSettings settings, HttpClient httpClient)
        {
            return new BasicAuthRequest(settings, httpClient);
        }

        public IClientRequest ServiceCredentials(ServiceCredentialsClient client, HttpClient httpClient)
        {
            return new ServiceCredentialsRequest(httpClient, client);
        }
    }
}