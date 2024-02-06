using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MrMirtel21.Utils.HttpConnector.Common
{
    public sealed class AuthToken
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; init; }

        [JsonProperty("token_type")]
        public string TokenType { get; init; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; init; }

        [JsonProperty("id_token")]
        public string IdToken { get; init; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; init; }
    }
}
