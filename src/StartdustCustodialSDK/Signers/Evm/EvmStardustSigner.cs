using Nethereum.Hex.HexConvertors.Extensions;
using StartdustCustodialSDK.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StartdustCustodialSDK.Signers.Evm
{
    public class EvmStardustSigner : AbstractStardustSigner
    {
        public string WalletId { get; set; }
        public StardustSignerAPI Api { get; set; }
        public ChainType ChainType { get; set; }
        public string ChainId { get; set; }

        public EvmStardustSigner()
        {

        }

        public EvmStardustSigner(string apiKey, string walletId, string chainId = "1", ChainType chainType = ChainType.Evm)
        {
            this.WalletId = walletId;
            this.Api = new StardustSignerAPI(apiKey);
            this.ChainId = chainId;
            this.ChainType = chainType;
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
            return await PrefixAndSign(message.ToByte());
        }

        public async Task<string> SignMessage(byte[] message)
        {
            return await PrefixAndSign(message);
        }

        private async Task<string> PrefixAndSign(byte[] message)
        {
            var messagePrefixed = PrefixedMessage(message);
            return await Sign(messagePrefixed);
        }

        private async Task<string> Sign(byte[] message)
        {
            var signPayload = new SignRequestPayload<string>(WalletId, ChainType, ChainId, message.ToHex());
            var signedMessage = await Api.SignMessage(signPayload);
            return signedMessage;
        }

        public byte[] PrefixedMessage(byte[] message)
        {
            var byteList = new List<byte>();
            var bytePrefix = "0x19".HexToByteArray();
            var textBytePrefix = Encoding.UTF8.GetBytes("Ethereum Signed Message:\n" + message.Length);

            byteList.AddRange(bytePrefix);
            byteList.AddRange(textBytePrefix);
            byteList.AddRange(message);
            return byteList.ToArray();
        }


    }
}
