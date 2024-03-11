using System;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StartdustCustodialSDK
{
    public class BaseStardustAPI
    {
        protected string ApiKey { get; set; }
        protected string Url { get; set; }

        public const string StardustUrl = "https://custodial-wallet.stardust.gg";

        public BaseStardustAPI(string apiKey, string url = StardustUrl)
        {
            ApiKey = apiKey;
            Url = url;

        }

        public async Task<T> ApiGet<T>(string endpoint) where T : class 
        {

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(Url);
                httpClient.DefaultRequestHeaders.Add("x-api-key", this.ApiKey);
                return await httpClient.GetFromJsonAsync<T>(endpoint);
            }
        }
    }
}
