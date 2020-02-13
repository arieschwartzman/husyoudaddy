using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Husyoudaddy.Data
{
    public class MarketplaceService
    {
        string accessToken;
        IConfidentialClientApplication clientApp;
        IConfiguration config;        

        public MarketplaceService(IConfiguration configuration) {
            clientApp = ConfidentialClientApplicationBuilder.Create(configuration["Marketplace:ClientId"])
                        .WithAuthority(AzureCloudInstance.AzurePublic, configuration["Marketplace:TenantId"])
                        .WithClientSecret(configuration["marketplace-saas-api"])
                        .Build();
            config = configuration;

        }

        public async Task<SaaSApplication> GetSaaSSubscription(string saasSubscriptionId)
        {
            string[] scopes = new string[] { config["Marketplace:Resource"] + "/.default" };
            var tokenResult = await clientApp.AcquireTokenForClient(scopes).ExecuteAsync();
            SaaSApplication app;
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenResult.AccessToken);
                using (var response = await client.GetAsync($"https://marketplaceapi.microsoft.com/api/saas/subscriptions/{saasSubscriptionId}?api-version=2018-08-31"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    app = JsonConvert.DeserializeObject<SaaSApplication>(apiResponse);
                }
            }
            return app;
        }
    }
}
