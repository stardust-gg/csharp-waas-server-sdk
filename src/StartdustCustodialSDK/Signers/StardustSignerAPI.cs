using StartdustCustodialSDK.Signers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StartdustCustodialSDK.Signers
{
    public class StardustSignerAPI : BaseStardustAPI
    {
        public StardustSignerAPI(string apiKey) : base(apiKey)
        {
        }

        public StardustSignerAPI(string apiKey, string url) : base(apiKey, url)
        {
        }

        public async Task<string> GetAddress(ApiRequestPayload payload)
        {
            return await ApiGet<ApiRequestPayload, string>("wallet/address", payload);
        }

        public async Task<string> GetPublicKey(ApiRequestPayload payload)
        {
            return await ApiGet<ApiRequestPayload, string>("wallet/public-key", payload);
        }

        public async Task<string> SignMessage(SignRequestPayload<string> payload)
        {
            return await ApiPost<SignRequestPayload<string>, string>("sign/message", payload);
        }

        public async Task<string> SignMessage(SignRequestPayload<byte[]> payload)
        {
            return await ApiPost<SignRequestPayload<byte[]>, string>("sign/message", payload);
        }
    }
}
