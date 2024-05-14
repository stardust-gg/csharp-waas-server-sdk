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
    public class StardustSuiSignerTest
    {
        private readonly ITestOutputHelper output;
        string apiKey;
        string walletId;

        public StardustSuiSignerTest(ITestOutputHelper output)
        {
            var config = TestConfigHelper.GetIConfigurationRoot();
            this.output = output;
            apiKey = config["ApiKey"];
            walletId = config["WalletId"];
        }


        [Fact]
        public async void Test()
        {
            byte[] val = BitConverter.GetBytes(1000);
            output.WriteLine(string.Join(",", val));
        }



    }
}