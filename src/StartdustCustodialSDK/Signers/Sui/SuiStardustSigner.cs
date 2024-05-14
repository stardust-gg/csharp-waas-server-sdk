using Blake2Core;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Signer;
using StartdustCustodialSDK.Signers.Sui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartdustCustodialSDK.Signers.Aptos
{
    public class SuiStardustSigner : AbstractStardustSigner
    {
        public string WalletId { get; set; }
        public StardustSignerAPI Api { get; set; }
        public ChainType ChainType { get; set; }

        public SuiStardustSigner() { }

        public SuiStardustSigner(string apiKey, string walletId)
        {
            WalletId = walletId;
            this.Api = new StardustSignerAPI(apiKey);
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

        public async Task<string> SignPersonalMessage(byte[] message)
        {
            if (message.Length > 255)
            {
                throw new ArgumentException("Message length too long, must not exceed 255 bytes");
            }
            var serializedMessage = message.ToList();
            serializedMessage.Insert(0, Convert.ToByte(message.Length));
            var intentMessage = Intent.MessageWithIntent(IntentScope.PersonalMessage, serializedMessage);
            Blake2BConfig config = new Blake2BConfig() { OutputSizeInBytes = 32 };
            var digest = Blake2B.ComputeHash(intentMessage, config);
            return await Sign(digest);
        }

        private async Task<string> Sign(byte[] message)
        {
            var signPayload = new SignRequestPayload<string>(WalletId, ChainType, message.ToHex());
            var signedMessage = await Api.SignMessage(signPayload);
            return signedMessage;
        }
    }
}
