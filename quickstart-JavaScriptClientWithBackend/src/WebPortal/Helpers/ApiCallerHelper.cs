using IdentityModel.Client;
using System.Text.Json;

namespace WebPortal.Helpers
{
    public static class ApiCallerHelper
    {
        private static async Task<DiscoveryDocumentResponse> GetDiscoverDocument(HttpClient client, string identityUrl = "https://localhost:6001")
        {
            // discover endpoints from metadata
            var discoveryDocument = await client.GetDiscoveryDocumentAsync(identityUrl);

            if (discoveryDocument.IsError)
            {
                throw new Exception(discoveryDocument.Error);
            }

            return discoveryDocument;
        }

        private static async Task<string> RequestTokenFromIdentityServer(HttpClient client, DiscoveryDocumentResponse discoveryDocument)
        {
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = discoveryDocument.TokenEndpoint,

                ClientId = "InvoiceServiceClient",
                ClientSecret = "secret",
                Scope = "OrderService"
            });

            if (tokenResponse.IsError)
            {
                throw new Exception(tokenResponse.Error);
            }

            return tokenResponse.AccessToken;
        }

        public static async Task<string?> CallingApi(string url = "https://localhost:7003/identity")
        {
            using (var client = new HttpClient())
            {
                var discoveryDocument = await GetDiscoverDocument(client);

                var tokenResponse = await RequestTokenFromIdentityServer(client, discoveryDocument);

                client.SetBearerToken(tokenResponse);

                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return response.StatusCode.ToString();
                }
                else
                {
                    var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync()).RootElement;
                    return JsonSerializer.Serialize(doc,  new JsonSerializerOptions { WriteIndented = true });
                }
            }
        }
    }
}