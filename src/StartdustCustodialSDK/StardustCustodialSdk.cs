using StartdustCustodialSDK.Application;
using StartdustCustodialSDK.Profile;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StartdustCustodialSDK
{
    public class StardustCustodialSdk
    {
        private StardustApplicationAPI stardustApplicationAPI;
        private StardustProfileAPI stardustProfileAPI;
        private StardustProfileIdentifierAPI stardustProfileIdentifierAPI;

        public StardustCustodialSdk(string apiKey, string url = BaseStardustAPI.StardustUrl)
        {
            stardustApplicationAPI = new StardustApplicationAPI(apiKey, url);
            stardustProfileAPI = new StardustProfileAPI(apiKey, url);
            stardustProfileIdentifierAPI = new StardustProfileIdentifierAPI(apiKey, url);
        }

        public async Task<StardustApplication> GetApplication()
        {
            return await stardustApplicationAPI.Get();
        }

        public async Task<StardustProfile> GetProfile(string profileId)
        {
            return await stardustProfileAPI.Get(profileId);
        }

        public async Task<StardustProfile> CreateProfile(string applicationId, string name = "")
        {
            var profileParams = new StardustProfileCreateParams(applicationId, name);
            return await stardustProfileAPI.Create(profileParams);
        }

        public async Task<StardustProfileIdentifier> GetProfileIdentifier(string profileIdentifierId)
        {
            return await stardustProfileIdentifierAPI.Get(profileIdentifierId);
        }

        public async Task<string> GenerateProfileJWT(string profileId, int duration)
        {
            return await stardustProfileAPI.GenerateClientJWT(profileId, duration);
        }
    }
}
