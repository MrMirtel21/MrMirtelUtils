using Microsoft.AspNetCore.Http;
using MrMirtel21.Utils.HttpConnector.Common;
using MrMirtel21.Utils.HttpConnector.Exceptions;

namespace MrMirtel21.Utils.HttpConnector.Requests
{
    public class PassThroughClientRequest : ClientRequest
    {
        private readonly IHttpContextAccessor _httpContextAccesor;

        public PassThroughClientRequest(HttpClient httpClient, IHttpContextAccessor httpContextAccesor) : base(httpClient)
        {
            _httpContextAccesor = httpContextAccesor;
        }

        protected override async Task<AuthToken> GetToken()
        {
            HttpContext context = _httpContextAccesor.HttpContext;
            string? authorizationHeader = context.Request.Headers["Authorization"].FirstOrDefault();
            if (authorizationHeader == null) 
            {
                throw new PassThroughTokenNotFoundException("Token not found.");
            }
            string token = authorizationHeader!.Replace("Bearer ", "");
            return await Task.FromResult(new AuthToken { AccessToken = token });
        }


    }
}