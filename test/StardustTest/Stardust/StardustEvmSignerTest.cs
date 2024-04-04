using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Nethereum.Accounts.AccountMessageSigning;
using Nethereum.BlockchainProcessing.BlockStorage.Entities;
using Nethereum.Contracts.QueryHandlers.MultiCall;
using Nethereum.Hex.HexTypes;
using Nethereum.JsonRpc.Client;
using Nethereum.RPC.AccountSigning;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Signer;
using Nethereum.Util;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using StardustTest.Config;
using StartdustCustodialSDK.Application;
using StartdustCustodialSDK.Signers.Evm;
using StartdustCustodialSDK.Signers.Nethereum;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Xunit.Abstractions;

namespace StardustTest.Stardust
{
    public class StardustEvmSignerTest
    {
        private readonly ITestOutputHelper output;
        string apiKey;
        string walletId;

        public StardustEvmSignerTest(ITestOutputHelper output)
        {
            var config = TestConfigHelper.GetIConfigurationRoot();
            this.output = output;
            apiKey = config["ApiKey"];
            walletId = config["WalletId"];
        }


        [Fact]
        public async void SignMessage()
        {
            if (!string.IsNullOrEmpty(apiKey) && !string.IsNullOrEmpty(walletId))
            {
                var evmSigner = new EvmStardustSigner(apiKey, walletId);
                var signer1 = new MessageSigner();
                var msg1 = "Hello world message 18/09/2017 02:55PM";
                var result = await evmSigner.SignRaw(msg1);
                var addressRec1 = signer1.HashAndEcRecover(msg1, result);
                var getAddress = await evmSigner.GetAddress();
                Assert.Equal(getAddress, addressRec1);
                output.WriteLine($"Sign message : {msg1}");
                output.WriteLine($"Address : {getAddress}");
                output.WriteLine($"Signature Raw : {result}");
            }
        }

        [Fact]
        public async void SignPrefixedMessage()
        {
            if (!string.IsNullOrEmpty(apiKey) && !string.IsNullOrEmpty(walletId))
            {
                var evmSigner = new EvmStardustSigner(apiKey, walletId);
                var signer1 = new EthereumMessageSigner();
                var msg1 = "Hello world message 18/09/2017 02:55PM";
                var result = await evmSigner.SignMessage(msg1);
                var addressRec1 = signer1.EncodeUTF8AndEcRecover(msg1, result);
                var getAddress = await evmSigner.GetAddress();
                var publicKey = await evmSigner.GetPublicKey();
                Assert.Equal(getAddress, addressRec1);
                Assert.NotNull(publicKey);
                output.WriteLine($"Sign message : {msg1}");
                output.WriteLine($"Address : {getAddress}");
                output.WriteLine($"Signature Prefixed : {result}");
            }
        }

    }
}