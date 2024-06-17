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
using StartdustCustodialSDK.Signers.Aptos;
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
    public class StardustAptosSignerTest
    {
        private readonly ITestOutputHelper output;
        private readonly AptosStardustSigner signer;
        string apiKey;
        string walletId;
        string url;

        public StardustAptosSignerTest(ITestOutputHelper output)
        {
            var config = TestConfigHelper.GetIConfigurationRoot();
            this.output = output;
            url = config["PROD_SYSTEM_STARDUST_API_URL"];
            apiKey = config["PROD_SYSTEM_STARDUST_API_KEY"];
            walletId = config["PROD_SYSTEM_STARDUST_WALLET_ID"];
            signer = new AptosStardustSigner(apiKey, walletId, url);
        }


        [Fact]
        public async void SignString()
        {

            if (!string.IsNullOrEmpty(apiKey) && !string.IsNullOrEmpty(walletId))
            {
                string message = "hello world";
                var signature = await signer.SignRaw(message);
                string expected = "0x66F5B43DCED4053792148BE533D6B257AE841773CF9E8E5D900146EA7524E0F55F2F20E7E075AD87419AE877F64BD539A071BBD3C64F2AA1B49ABA7CA8404F0E";
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
                string expected = "0x66F5B43DCED4053792148BE533D6B257AE841773CF9E8E5D900146EA7524E0F55F2F20E7E075AD87419AE877F64BD539A071BBD3C64F2AA1B49ABA7CA8404F0E";
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
                string expected = "0x0dcff96726d8a22d0275a80f4b1787d80933080da4af6711598dad8e4711951c";
                Assert.Equal(key, expected);
            }
        }

        [Fact]
        public async void GetAddress()
        {

            if (!string.IsNullOrEmpty(apiKey) && !string.IsNullOrEmpty(walletId))
            {
                string key = await signer.GetAddress();
                string expected = "0x43171a4fce80da99cb1976f6418a3b386c4b36c2529667ac925b2fefcb918463";
                Assert.Equal(key, expected);
            }
        }
    }
}