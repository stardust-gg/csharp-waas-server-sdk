using StartdustCustodialSDK.Application;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StartdustCustodialSDK.Profile
{
    public class StardustProfileIdentifierAPI : BaseStardustAPI
    {
        public StardustProfileIdentifierAPI(string apiKey) : base(apiKey)
        {
        }

        public StardustProfileIdentifierAPI(string apiKey, string url) : base(apiKey, url)
        {
        }

        public async Task<StardustProfileIdentifier> Create(StardustProfileIdentifierCreateParams profileIdentifierParams)
        {
            var profileIdentifier = await ApiPost<StardustProfileIdentifier, StardustProfileIdentifierCreateParams>("profile/identifier", profileIdentifierParams);
            return profileIdentifier;
        }

        public async Task<StardustProfileIdentifier> Get(string profileIdentifierId)
        {
            var profileIdentifier = await ApiGet<StardustProfileIdentifier>($"profile/identifier/{profileIdentifierId}");
            return profileIdentifier;
        }

        public async Task<List<StardustProfileIdentifier>> List(StardustProfileIdentifierListParams profileIdentifierListParams)
        {
            var profileIdentifiers = await ApiGet<List<StardustProfileIdentifier>>($"profile/identifier/{profileIdentifierListParams}");
            return profileIdentifiers;
        }

    }
}
