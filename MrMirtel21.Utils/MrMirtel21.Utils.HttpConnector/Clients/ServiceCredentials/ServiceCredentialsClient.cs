using Jose;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using System.Security.Cryptography;

namespace MrMirtel21.Utils.HttpConnector.Clients.ServiceCredentials
{
    public class ServiceCredentialsClient
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string IdpEndpoint { get; set; }
        public Dictionary<object, object> Payload { get; set; }
        public string PrivateKey { get; set; }

        public ServiceCredentialsClient(string clientId, string clientSecret, string idpEndpoint, Dictionary<object, object> payload, string privateKey)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
            IdpEndpoint = idpEndpoint;
            Payload = payload;
            PrivateKey = privateKey;
        }

        public async Task<string> GetAccessToken()
        {
            var jwtToken = EncodePayload();
            var content = new MultipartFormDataContent
            {
                { new StringContent(ClientId), "client_id" },
                { new StringContent(ClientSecret), "client_secret" },
                { new StringContent(jwtToken), "jwt_token" }
            };

            var client = new HttpClient();

            var resposne = await client.PostAsync(IdpEndpoint, content);
            return await resposne.Content.ReadAsStringAsync();
        }

        private string EncodePayload()
        {
            RSAParameters rsaParams;
            using (var tr = new StringReader(PrivateKey))
            {
                var pemReader = new PemReader(tr);
                if (!(pemReader.ReadObject() is AsymmetricCipherKeyPair keyPair))
                {
                    throw new Exception("Could not read RSA private key");
                }

                var privateRsaParams = keyPair.Private as RsaPrivateCrtKeyParameters;
                rsaParams = DotNetUtilities.ToRSAParameters(privateRsaParams);
            }
            using RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(rsaParams);

            return JWT.Encode(Payload, rsa, JwsAlgorithm.RS256);
        }
    }
}
