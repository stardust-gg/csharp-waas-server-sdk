using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StartdustCustodialSDK.Application
{
    public class StardustApplicationAPI : BaseStardustAPI
    {
        public StardustApplicationAPI(string apiKey) : base(apiKey)
        {
        }

        public StardustApplicationAPI(string apiKey, string url) : base(apiKey, url)
        {
        }

        public async Task<StardustApplication> Get()
        {
            var application = await ApiGet<StardustApplication>("application");
            application.Init(ApiKey);
            return application;
        }
    }
}
