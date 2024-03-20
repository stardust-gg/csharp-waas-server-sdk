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
            var profileIdentifier = await ApiPost<StardustProfileIdentifierCreateParams, StardustProfileIdentifier>("profile/identifier", profileIdentifierParams);
            return profileIdentifier;
        }

        public async Task<StardustProfileIdentifier> Get(string profileIdentifierId)
        {
            var profileIdentifier = await ApiGet<StardustProfileIdentifier>($"profile/identifier/{profileIdentifierId}");
            return profileIdentifier;
        }

        public async Task<List<StardustProfileIdentifier>> List(StardustProfileIdentifierListParams profileIdentifierListParams)
        {
            var profileIdentifiers = await ApiGet<StardustProfileIdentifierListParams, StartdustProfileIdentifierListResult>($"profile/identifier", profileIdentifierListParams);
            return profileIdentifiers.Results;
        }

    }
}
