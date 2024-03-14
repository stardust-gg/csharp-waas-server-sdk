using StartdustCustodialSDK.Application;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StartdustCustodialSDK.Profile
{
    public class StardustProfileAPI : BaseStardustAPI
    {
        public StardustProfileAPI(string apiKey) : base(apiKey)
        {
        }

        public StardustProfileAPI(string apiKey, string url) : base(apiKey, url)
        {
        }

        public async Task<StardustProfile> Create(StardustProfileCreateParams profileParams)
        {
            var profile = await ApiPost<StardustProfile, StardustProfileCreateParams>("profile", profileParams);
            profile.Init(this.ApiKey);
            return profile;
        }

        public async Task<StardustProfile> Get(string profileId)
        {
            var profile = await ApiGet<StardustProfile>($"profile/{profileId}?expand=identifiers,wallets");
            profile.Init(this.ApiKey);
            return profile;
        }

        public async Task<string> GenerateClientJWT(string profileId, long duration)
        {
            var result = await ApiPost<StardustToken, StardustTokenParams>($"profile/{profileId}/authenticate", new StardustTokenParams(duration));
            return result.Jwt;
        }

    }
}
