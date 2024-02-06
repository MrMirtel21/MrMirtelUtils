using Microsoft.AspNetCore.Http;
using MrMirtel21.Utils.HttpConnector.Common;

namespace MrMirtel21.Utils.HttpConnector.Requests
{
    public class NoAuthRequest : ClientRequest
    {
        private readonly IHttpContextAccessor _httpContextAccesor;

        public NoAuthRequest(HttpClient httpClient, IHttpContextAccessor httpContextAccesor) : base(httpClient)
        {
            _httpContextAccesor = httpContextAccesor;
        }

        protected override async Task<AuthToken> GetToken()
        {
            return await Task.FromResult(new AuthToken
            {
                AccessToken = "none",
                IdToken = "none",
                ExpiresIn = 1,
                RefreshToken = "none",
                TokenType = "none"
            });
        }
    }
}