using Nethereum.BlockchainProcessing.BlockStorage.Entities;
using Nethereum.Hex.HexTypes;
using Nethereum.JsonRpc.Client;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Util;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using StardustTest.Config;
using StartdustCustodialSDK.Application;
using StartdustCustodialSDK.Signers.Nethereum;
using System.Diagnostics;
using Xunit.Abstractions;

namespace StardustTest.Stardust
{
    public class StardustNethereumTest
    {
        private readonly ITestOutputHelper output;
        string apiKey;
        string walletId;

        public StardustNethereumTest(ITestOutputHelper output)
        {
            var config = TestConfigHelper.GetIConfigurationRoot();
            this.output = output;
            apiKey = config["ApiKey"];
            walletId = config["WalletId"];
        }

        [Fact]
        public async void GetMyApp()
        {
            // Use wallet id with credit on mumbai
            if (!string.IsNullOrEmpty(apiKey) && !string.IsNullOrEmpty(walletId))
            {
                string rpcAddress = "https://rpc.ankr.com/polygon_mumbai";

                var rpcClient = new RpcClient(new Uri(rpcAddress));

                // use walletId with token to test this part (80001 for mumbai)
                var nethereumSigner = new NethereumStardustSigner(apiKey, walletId, "0x13881");
                var externalAccount = new ExternalAccount(nethereumSigner);
                await externalAccount.InitialiseAsync();

                externalAccount.InitialiseDefaultTransactionManager(rpcClient);

                // Initialize Web3
                var web3 = new Web3(externalAccount, rpcClient);
                web3.TransactionManager.UseLegacyAsDefault = true;

                output.WriteLine(externalAccount.Address);
                var toAddress = "0x0f571D2625b503BB7C1d2b5655b483a2Fa696fEf"; // Replace with the address of the recipient
                var amountToSend = 0.1m; // Replace with the amount to send in ether

                var gasPriceGwei = 10; // Replace with your desired gas price in Gwei
                var gasLimit = 21000; // Replace with your desired gas limit

                var transactionInput = new Nethereum.RPC.Eth.DTOs.TransactionInput()
                {
                    From = externalAccount.Address,
                    To = toAddress,
                    ChainId = new HexBigInteger("0x13881"),
                    Value = new HexBigInteger(Web3.Convert.ToWei(amountToSend)),
                    GasPrice = new HexBigInteger(Web3.Convert.ToWei(gasPriceGwei, UnitConversion.EthUnit.Gwei)),
                    Gas = new HexBigInteger(gasLimit)
                };

                var signedTransaction = await web3.TransactionManager.SignTransactionAsync(transactionInput);


                output.WriteLine($"Signed Transaction: {signedTransaction}");

                var transaction = await web3.Eth.Transactions.SendRawTransaction.SendRequestAsync(signedTransaction);
                var receipt = await GetReceiptAsync(web3, transaction);
                var balance = await web3.Eth.GetBalance.SendRequestAsync(toAddress);
                Assert.True(balance.Value > 0);

            }
        }

        private async Task<TransactionReceipt> GetReceiptAsync(Web3 web3, string transactionHash)
        {

            var receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);

            while (receipt == null)
            {
                Thread.Sleep(1000);
                receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);
            }
            return receipt;
        }

    }
}