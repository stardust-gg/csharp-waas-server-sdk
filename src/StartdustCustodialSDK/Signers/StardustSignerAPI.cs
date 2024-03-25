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
            var response = await ApiGet<ApiRequestPayload, AddressResponse>("wallet/address", payload);
            return response.Address;
        }

        public async Task<string> GetPublicKey(ApiRequestPayload payload)
        {
            var response = await ApiGet<ApiRequestPayload, PublicKeyResponse>("wallet/public-key", payload);
            return response.PublicKey;
        }

        public async Task<string> SignMessage(SignRequestPayload<string> payload)
        {
            var response = await ApiPost<SignRequestPayload<string>, SignatureResponse>("sign/message", payload);
            return response.Signature;
        }

        public async Task<string> SignMessage(SignRequestPayload<byte[]> payload)
        {
            var response = await ApiPost<SignRequestPayload<byte[]>, SignatureResponse>("sign/message", payload);
            return response.Signature;
        }
    }
}
