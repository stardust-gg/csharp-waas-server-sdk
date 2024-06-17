using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Signer;
using StartdustCustodialSDK.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StartdustCustodialSDK.Signers.Aptos
{
    public class AptosStardustSigner : AbstractStardustSigner
    {
        public string WalletId { get; set; }
        public StardustSignerAPI Api { get; set; }
        public ChainType ChainType { get; set; }

        public AptosStardustSigner() { }

        public AptosStardustSigner(string apiKey, string walletId, string url = BaseStardustAPI.StardustUrl)
        {
            WalletId = walletId;
            this.Api = new StardustSignerAPI(apiKey, url);
            this.ChainType = ChainType.Aptos;
        }

        public override async Task<string> GetAddress()
        {
            var payload = new ApiRequestPayload(this.WalletId, this.ChainType);
            var address = await Api.GetAddress(payload);
            return address;
        }

        public override async Task<string> GetPublicKey()
        {
            var payload = new ApiRequestPayload(this.WalletId, this.ChainType);
            var publicKey = await Api.GetPublicKey(payload);
            return publicKey;
        }

        public override async Task<string> SignRaw(byte[] message)
        {
            return await Sign(message);
        }

        public override async Task<string> SignRaw(string message)
        {
            var messageUtf8 = Encoding.UTF8.GetBytes(message);
            return await Sign(messageUtf8);
        }

        public async Task<string> SignMessage(string message)
        {
            return await Sign(message.ToByte());
        }

        public async Task<string> SignMessage(byte[] message)
        {
            return await Sign(message);
        }

        private async Task<string> Sign(byte[] message)
        {
            var signPayload = new SignRequestPayload<string>(WalletId, ChainType, message.ToHex());
            var signedMessage = await Api.SignMessage(signPayload);
            return signedMessage;
        }

        public ApiRequestPayload ToJson()
        {
            return new ApiRequestPayload(WalletId, ChainType);
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(ToJson());
        }
    }
}
