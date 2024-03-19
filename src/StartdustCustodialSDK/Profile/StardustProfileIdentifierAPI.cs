using StartdustCustodialSDK.Application;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

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
            var data = new Dictionary<string, string>();
            data.Add("profileId", profileIdentifierListParams.ProfileId);
            data.Add("start", profileIdentifierListParams.Start.ToString());
            data.Add("limit", profileIdentifierListParams.Limit.ToString());          
            var profileIdentifiers = await ApiGet<StartdustProfileIdentifierListResult>($"profile/identifier",data);
            return profileIdentifiers.Results;
        }

    }
}
