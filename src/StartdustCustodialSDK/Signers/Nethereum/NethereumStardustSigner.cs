using Nethereum.Hex.HexConvertors.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StartdustCustodialSDK.Signers.Nethereum
{
    public class NethereumStardustSigner : AbstractStardustSigner
    {
        public string WalletId { get; set; }

        public StardustSignerAPI Api { get; set; }

        public ChainType ChainType { get; set; }

        public NethereumStardustSigner()
        {

        }

        public NethereumStardustSigner(string walletId, string apiKey)
        {
            this.WalletId = walletId;
            this.Api = new StardustSignerAPI(apiKey);
            this.ChainType = ChainType.Evm;
        }

        public override Task<string> GetAddress()
        {
            var payload = new ApiRequestPayload(this.WalletId, this.ChainType);
            return Api.GetAddress(payload);
        }

        public override Task<string> GetPublicKey()
        {
            var payload = new ApiRequestPayload(this.WalletId, this.ChainType);
            return Api.GetPublicKey(payload);
        }

        public override async Task<string> SignRaw(byte[] message)
        {
            var messagePrefix = "\x19Ethereum Signed Message:\n";

            string messageContent = message.ToHex().Replace("0x", "");
            int messageLen = message.Length;

            UTF8Encoding.UTF8.GetBytes(messagePrefix);

            var payload = new SignRequestPayload<byte[]>() { ChainType = this.ChainType, Message = message, WalletId = WalletId };
            var result = await Api.SignMessage(payload);
            return result;
        }

        public override Task<string> SignRaw(string message)
        {
            throw new NotImplementedException();
        }

        //private string CreatePrefixedMessage<T>(T message)
        //{
        //    var messagePrefix = "\x19Ethereum Signed Message:\n";

        //    string messageContent;
        //    int messageLen;

        //    if (message is byte[])
        //    {
        //        var messageByte = message as byte[];
        //        messageContent = messageByte.ToHex().Replace("0x", "");
        //        messageLen = messageByte.Length;
        //    }
        //    else
        //    {
        //        var messageString = message as string;
        //        messageContent = IsHexString(messageString)
        //          ? messageString.Replace("0x", "")
        //          : convertStringToHexString(message).replace(/ ^0x /, '');
        //        messageLen = messageContent.length / 2; // Byte length for hex string
        //    }

        //    var prefixedMessage =
        //      convertStringToHexString(messagePrefix) +
        //      new HexString(convertStringToHexString(String(messageLen))).strip() +
        //      new HexString(messageContent).strip();

        //    return prefixedMessage;
        //}

        private bool IsHexString(string str)
        {
            var hexRegex = "^0x[a-fA-F0-9]+$";
            return Regex.Match(str, hexRegex).Success;
        }

       
    }
}
