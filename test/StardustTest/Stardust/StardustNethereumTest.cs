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
using StartdustCustodialSDK.Signers.Nethereum;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
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
        public async void SendTransaction()
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

                var getAddress = await nethereumSigner.GetAddressAsync();

                Assert.Equal(externalAccount.Address, getAddress);

                output.WriteLine(externalAccount.Address);
                var toAddress = "0x0f571D2625b503BB7C1d2b5655b483a2Fa696fEf"; // Replace with the address of the recipient
                var balance = await web3.Eth.GetBalance.SendRequestAsync(toAddress);
                var amountToSend = 0.001m; // Replace with the amount to send in ether

                var gasPriceGwei = 2; // Replace with your desired gas price in Gwei
                var gasLimit = 21000; // Replace with your desired gas limit

                var amount = Web3.Convert.ToWei(amountToSend);
                var transactionInput = new Nethereum.RPC.Eth.DTOs.TransactionInput()
                {
                    From = externalAccount.Address,
                    To = toAddress,
                    ChainId = new HexBigInteger("0x13881"),
                    Value = amount.ToHexBigInteger(),
                    GasPrice = new HexBigInteger(Web3.Convert.ToWei(gasPriceGwei, UnitConversion.EthUnit.Gwei)),
                    Gas = new HexBigInteger(gasLimit)
                };

                var signedTransaction = await web3.TransactionManager.SignTransactionAsync(transactionInput);

                output.WriteLine($"Address : {getAddress}");
                output.WriteLine($"Signed Transaction: {signedTransaction}");

                var transaction = await web3.Eth.Transactions.SendRawTransaction.SendRequestAsync(signedTransaction);
                output.WriteLine($"Transaction Id: {transaction}");
                var receipt = await GetReceiptAsync(web3, transaction);
                var balanceAfter = await web3.Eth.GetBalance.SendRequestAsync(toAddress);
                var sendAmount = balanceAfter.Value - balance.Value;
                Assert.Equal(sendAmount, amount);

            }
        }

        [Fact]
        public async void SignMessage()
        {
            if (!string.IsNullOrEmpty(apiKey) && !string.IsNullOrEmpty(walletId))
            {
                var nethereumSigner = new NethereumStardustSigner(apiKey, walletId);
                var signer1 = new MessageSigner();
                var msg1 = "wee test message 18/09/2017 02:55PM";
                var result = await nethereumSigner.EvmSigner.SignRaw(msg1);
                var addressRec1 = signer1.HashAndEcRecover(msg1, result);
                var getAddress = await nethereumSigner.GetAddressAsync();
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
                var nethereumSigner = new NethereumStardustSigner(apiKey, walletId);
                var signer1 = new EthereumMessageSigner();
                var msg1 = "wee test message 18/09/2017 02:55PM";
                var result = await nethereumSigner.EvmSigner.SignMessage(msg1);
                var addressRec1 = signer1.EncodeUTF8AndEcRecover(msg1, result);
                var getAddress = await nethereumSigner.GetAddressAsync();
                Assert.Equal(getAddress, addressRec1);
                output.WriteLine($"Sign message : {msg1}");
                output.WriteLine($"Address : {getAddress}");
                output.WriteLine($"Signature Prefixed : {result}");
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