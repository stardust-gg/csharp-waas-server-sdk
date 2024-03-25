using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Model;
using Nethereum.Signer;
using Nethereum.Signer.Crypto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StartdustCustodialSDK.Signers.Nethereum
{
    public class NethereumStardustSigner : EthExternalSignerBase
    {
        public string WalletId { get; set; }
        public StardustSignerAPI Api { get; set; }
        public ChainType ChainType { get; set; }
        public string ChainId { get; set; }

        public override bool Supported1559 => true;
        public override ExternalSignerTransactionFormat ExternalSignerTransactionFormat { get; protected set; } = ExternalSignerTransactionFormat.RLP;
        public override bool CalculatesV { get; protected set; } = false;

        public NethereumStardustSigner()
        {

        }

        public NethereumStardustSigner(string apiKey, string walletId, string chainId = "1", ChainType chainType = ChainType.Evm)
        {
            this.WalletId = walletId;
            this.Api = new StardustSignerAPI(apiKey);
            this.ChainId = chainId;
            this.ChainType = chainType;
        }

        protected override async Task<byte[]> GetPublicKeyAsync()
        {
            var payload = new ApiRequestPayload(this.WalletId, this.ChainType);
            var publicKey = await Api.GetPublicKey(payload);
            return publicKey.HexToByteArray();
        }

        protected override async Task<ECDSASignature> SignExternallyAsync(byte[] bytes)
        {
            var signPayload = new SignRequestPayload<string>(WalletId, ChainType, ChainId, bytes.ToHex());
            var signedMessage = await Api.SignMessage(signPayload);
            var signature = ECDSASignatureFactory.FromComponents(signedMessage.HexToByteArray()).MakeCanonical();
            return signature;
        }

        public override async Task SignAsync(LegacyTransactionChainId transaction)
        {
            await SignRLPTransactionAsync(transaction);
        }

        public override async Task SignAsync(LegacyTransaction transaction)
        {
            await SignRLPTransactionAsync(transaction);
        }

        public override async Task SignAsync(Transaction1559 transaction)
        {
            await SignRLPTransactionAsync(transaction);
        }
    }
}
