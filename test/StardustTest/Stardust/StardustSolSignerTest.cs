using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Nethereum.Accounts.AccountMessageSigning;
using Nethereum.BlockchainProcessing.BlockStorage.Entities;
using Nethereum.Contracts.QueryHandlers.MultiCall;
using Nethereum.Hex.HexConvertors.Extensions;
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
using StartdustCustodialSDK.Signers;
using StartdustCustodialSDK.Signers.Evm;
using StartdustCustodialSDK.Signers.Nethereum;
using StartdustCustodialSDK.Signers.Sol;
using StartdustCustodialSDK.Signers.Sui;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Xunit.Abstractions;

namespace StardustTest.Stardust
{
    public class StardustSolSignerTest
    {
        private readonly ITestOutputHelper output;
        private readonly SolStardustSigner signer;
        string apiKey;
        string walletId;
        string url;

        public StardustSolSignerTest(ITestOutputHelper output)
        {
            var config = TestConfigHelper.GetIConfigurationRoot();
            this.output = output;
            url = config["PROD_SYSTEM_STARDUST_API_URL"];
            apiKey = config["PROD_SYSTEM_STARDUST_API_KEY"];
            walletId = config["PROD_SYSTEM_STARDUST_WALLET_ID"];
            signer = new SolStardustSigner(apiKey, walletId, url);
        }


        [Fact]
        public async void SignString()
        {

            if (!string.IsNullOrEmpty(apiKey) && !string.IsNullOrEmpty(walletId))
            {
                string message = "hello world";
                var signature = await signer.SignRaw(message);
                string expected = "0x3536DEA1D085CC481F148BD616387AB8441F38FA1A7265241A38077290B61DE716AF08EBE7277FA06592CC3E3361D81AC24961B60C468FF0DC18742E94DBC90C";
                output.WriteLine(signature);
                Assert.Equal(signature, expected);
            }
        }


        [Fact]
        public async void SignHexa()
        {

            if (!string.IsNullOrEmpty(apiKey) && !string.IsNullOrEmpty(walletId))
            {
                string message = "0x68656c6c6f20776f726c64";
                var signature = await signer.SignRaw(message.HexToByteArray());
                string expected = "0x3536DEA1D085CC481F148BD616387AB8441F38FA1A7265241A38077290B61DE716AF08EBE7277FA06592CC3E3361D81AC24961B60C468FF0DC18742E94DBC90C";
                output.WriteLine(signature);
                Assert.Equal(signature, expected);
            }
        }


        [Fact]
        public async void GetPublickKey()
        {

            if (!string.IsNullOrEmpty(apiKey) && !string.IsNullOrEmpty(walletId))
            {
                string key = await signer.GetPublicKey();
                string expected = "8Jh1N5XzkGsvsnAeA6EPdF8Y8yxfQGi2ug4bppng2TDv";
                Assert.Equal(key, expected);
            }
        }

        [Fact]
        public async void GetAddress()
        {

            if (!string.IsNullOrEmpty(apiKey) && !string.IsNullOrEmpty(walletId))
            {
                string key = await signer.GetAddress();
                string expected = "8Jh1N5XzkGsvsnAeA6EPdF8Y8yxfQGi2ug4bppng2TDv";
                Assert.Equal(key, expected);
            }
        }
    }
}