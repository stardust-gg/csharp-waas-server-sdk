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
using StartdustCustodialSDK.Signers.Sui;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Xunit.Abstractions;

namespace StardustTest.Stardust
{
    public class StardustSuiSignerTest
    {
        private readonly ITestOutputHelper output;
        private readonly SuiStardustSigner signer;
        string apiKey;
        string walletId;
        string url;

        public StardustSuiSignerTest(ITestOutputHelper output)
        {
            var config = TestConfigHelper.GetIConfigurationRoot();
            this.output = output;
            url = config["PROD_SYSTEM_STARDUST_API_URL"];
            apiKey = config["PROD_SYSTEM_STARDUST_API_KEY"];
            walletId = config["PROD_SYSTEM_STARDUST_WALLET_ID"];
            signer = new SuiStardustSigner(apiKey, url, walletId);
        }


        [Fact]
        public async void SignString()
        {

            if (!string.IsNullOrEmpty(apiKey) && !string.IsNullOrEmpty(walletId))
            {
                string message = "hello world";
                var signature = await signer.SignRaw(message);
                string expected = "ADU23qHQhcxIHxSL1hY4erhEHzj6GnJlJBo4B3KQth3nFq8I6+cnf6Blksw+M2HYGsJJYbYMRo/w3Bh0LpTbyQxsierfKdh0UkIxFtapQZvTmbfjwzW++UogcFzyY3FMjQ==";
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
                string expected = "ADU23qHQhcxIHxSL1hY4erhEHzj6GnJlJBo4B3KQth3nFq8I6+cnf6Blksw+M2HYGsJJYbYMRo/w3Bh0LpTbyQxsierfKdh0UkIxFtapQZvTmbfjwzW++UogcFzyY3FMjQ==";
                output.WriteLine(signature);
                Assert.Equal(signature, expected);
            }
        }
    }
}