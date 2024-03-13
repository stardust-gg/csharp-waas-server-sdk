using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

        public async Task<T> ApiGet<T>(string endpoint, Dictionary<string, string> query = null) where T : class
        {

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(Url);
                httpClient.DefaultRequestHeaders.Add("x-api-key", this.ApiKey);
                if (query?.Count > 0)
                {
                    // format query parameters to match html format 
                    string queryString = string.Join("&", query.Select(p => $"{p.Key}={Uri.EscapeDataString(p.Value)}"));
                    return await httpClient.GetFromJsonAsync<T>($"{endpoint}?{queryString}");
                }
                else
                {
                    return await httpClient.GetFromJsonAsync<T>(endpoint);
                }
            }
        }

        public async Task<T> ApiPost<T, U>(string endpoint, U data = null) where T : class where U : class
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(Url);
                httpClient.DefaultRequestHeaders.Add("x-api-key", this.ApiKey);
                HttpResponseMessage response;
                if (data != null)
                {
                    response = await httpClient.PostAsJsonAsync<U>(endpoint, data);
                }
                else
                {
                    response = await httpClient.PostAsync(endpoint, null);
                }
                response.EnsureSuccessStatusCode();
                var jsonResponse = await response.Content.ReadAsStringAsync();
                T result = JsonSerializer.Deserialize<T>(jsonResponse,new JsonSerializerOptions(JsonSerializerDefaults.Web));
                return result;
            }
        }
    }
}
