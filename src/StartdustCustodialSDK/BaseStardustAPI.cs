using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

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

        public async Task<TOut> ApiGet<TIn, TOut>(string endpoint, TIn data) where TOut : class where TIn : class
        {

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(Url);
                httpClient.DefaultRequestHeaders.Add("x-api-key", this.ApiKey);
                // format query parameters to match html format 
                string queryString = ToQueryString(data);

                // for test
                //var result = await httpClient.GetAsync($"{endpoint}?{queryString}");
                //var json =await result.Content.ReadAsStringAsync();

                return await httpClient.GetFromJsonAsync<TOut>($"{endpoint}?{queryString}");
            }
        }

        public async Task<TOut> ApiPost<TIn, TOut>(string endpoint, TIn data = null) where TOut : class where TIn : class
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(Url);
                httpClient.DefaultRequestHeaders.Add("x-api-key", this.ApiKey);
                HttpResponseMessage response;
                if (data != null)
                {
                    response = await httpClient.PostAsJsonAsync<TIn>(endpoint, data);
                }
                else
                {
                    response = await httpClient.PostAsync(endpoint, null);
                }
                response.EnsureSuccessStatusCode();
                var jsonResponse = await response.Content.ReadAsStringAsync();
                TOut result = JsonSerializer.Deserialize<TOut>(jsonResponse, new JsonSerializerOptions(JsonSerializerDefaults.Web));
                return result;
            }
        }

        private string ToQueryString<T>(T obj)
        {
            string jsonString = JsonSerializer.Serialize<T>(obj, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            });
            using (var jsonObject = JsonDocument.Parse(jsonString))
            {
                var properties = jsonObject
                    .RootElement.EnumerateObject()
                    .Where(p => p.Value.ValueKind != JsonValueKind.Null)
                    .Select(p =>
                        $"{HttpUtility.UrlEncode(p.Name)}={HttpUtility.UrlEncode(p.Value.ToString())}");
                return string.Join("&", properties);
            }
        }
    }
}
