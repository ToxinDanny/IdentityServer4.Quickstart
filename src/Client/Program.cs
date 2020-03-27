using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

namespace Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5000"); 
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                throw new ArgumentNullException();
            }

            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,

                ClientId = "client",
                ClientSecret = "secret",
                Scope = "api1"
            });
            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
            }

            Console.WriteLine(tokenResponse.Json);

            client.SetBearerToken(tokenResponse.AccessToken);
            var response = await client.GetAsync("http://localhost:5001/identity");
            
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(JArray.Parse(content));

        }

        //private static async Task<TokenResponse> GetToken(HttpClient client, DiscoveryDocumentResponse disco)
        //{
        //    var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        //    {
        //        Address = disco.TokenEndpoint,
        //        ClientId = "client",
        //        ClientSecret = "secret",
        //        Scope = "api1"
        //    });

            
        //    return tokenResponse;
        //}
    }
}
