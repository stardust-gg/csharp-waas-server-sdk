// See https://aka.ms/new-console-template for more information
using Nethereum.Hex.HexTypes;
using Nethereum.JsonRpc.Client;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Util;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using StartdustCustodialSDK;
using StartdustCustodialSDK.Signers.Nethereum;
using System.Text.Json;

Console.WriteLine("Mint a nft on mumbai with nethereum");

try
{
    var apiKey = args[0];
    var walletId = args[1];

    var stardustCustodialSdk = new StardustCustodialSdk(apiKey);
    var wallet = await stardustCustodialSdk.GetWallet(walletId);
    var nethereumSigner = wallet.Nethereum;

    string rpcAddress = "https://rpc.ankr.com/polygon_mumbai";

    var rpcClient = new RpcClient(new Uri(rpcAddress));

    var externalAccount = new ExternalAccount(nethereumSigner);
    await externalAccount.InitialiseAsync();

    externalAccount.InitialiseDefaultTransactionManager(rpcClient);

    // Initialize Web3
    var web3 = new Web3(externalAccount, rpcClient);
    web3.TransactionManager.UseLegacyAsDefault = true;

    Console.WriteLine($"Wallet account address {externalAccount.Address}");
    var toAddress = "0x55bb1b0d63ac51f3603d30cb335255da26c2e35e";// nft smartcontract

    var gasPriceGwei = 2;
    var gasLimit = 100000;

    var abi = @"[{""inputs"":[],""stateMutability"":""nonpayable"",""type"":""constructor""},{""inputs"":[{""internalType"":""address"",""name"":""sender"",""type"":""address""},{""internalType"":""uint256"",""name"":""tokenId"",""type"":""uint256""},{""internalType"":""address"",""name"":""owner"",""type"":""address""}],""name"":""ERC721IncorrectOwner"",""type"":""error""},{""inputs"":[{""internalType"":""address"",""name"":""operator"",""type"":""address""},{""internalType"":""uint256"",""name"":""tokenId"",""type"":""uint256""}],""name"":""ERC721InsufficientApproval"",""type"":""error""},{""inputs"":[{""internalType"":""address"",""name"":""approver"",""type"":""address""}],""name"":""ERC721InvalidApprover"",""type"":""error""},{""inputs"":[{""internalType"":""address"",""name"":""operator"",""type"":""address""}],""name"":""ERC721InvalidOperator"",""type"":""error""},{""inputs"":[{""internalType"":""address"",""name"":""owner"",""type"":""address""}],""name"":""ERC721InvalidOwner"",""type"":""error""},{""inputs"":[{""internalType"":""address"",""name"":""receiver"",""type"":""address""}],""name"":""ERC721InvalidReceiver"",""type"":""error""},{""inputs"":[{""internalType"":""address"",""name"":""sender"",""type"":""address""}],""name"":""ERC721InvalidSender"",""type"":""error""},{""inputs"":[{""internalType"":""uint256"",""name"":""tokenId"",""type"":""uint256""}],""name"":""ERC721NonexistentToken"",""type"":""error""},{""anonymous"":false,""inputs"":[{""indexed"":true,""internalType"":""address"",""name"":""owner"",""type"":""address""},{""indexed"":true,""internalType"":""address"",""name"":""approved"",""type"":""address""},{""indexed"":true,""internalType"":""uint256"",""name"":""tokenId"",""type"":""uint256""}],""name"":""Approval"",""type"":""event""},{""anonymous"":false,""inputs"":[{""indexed"":true,""internalType"":""address"",""name"":""owner"",""type"":""address""},{""indexed"":true,""internalType"":""address"",""name"":""operator"",""type"":""address""},{""indexed"":false,""internalType"":""bool"",""name"":""approved"",""type"":""bool""}],""name"":""ApprovalForAll"",""type"":""event""},{""anonymous"":false,""inputs"":[{""indexed"":true,""internalType"":""address"",""name"":""from"",""type"":""address""},{""indexed"":true,""internalType"":""address"",""name"":""to"",""type"":""address""},{""indexed"":true,""internalType"":""uint256"",""name"":""tokenId"",""type"":""uint256""}],""name"":""Transfer"",""type"":""event""},{""inputs"":[{""internalType"":""address"",""name"":""to"",""type"":""address""},{""internalType"":""uint256"",""name"":""tokenId"",""type"":""uint256""}],""name"":""approve"",""outputs"":[],""stateMutability"":""nonpayable"",""type"":""function""},{""inputs"":[{""internalType"":""address"",""name"":""owner"",""type"":""address""}],""name"":""balanceOf"",""outputs"":[{""internalType"":""uint256"",""name"":"""",""type"":""uint256""}],""stateMutability"":""view"",""type"":""function""},{""inputs"":[{""internalType"":""uint256"",""name"":""tokenId"",""type"":""uint256""}],""name"":""getApproved"",""outputs"":[{""internalType"":""address"",""name"":"""",""type"":""address""}],""stateMutability"":""view"",""type"":""function""},{""inputs"":[{""internalType"":""address"",""name"":""owner"",""type"":""address""},{""internalType"":""address"",""name"":""operator"",""type"":""address""}],""name"":""isApprovedForAll"",""outputs"":[{""internalType"":""bool"",""name"":"""",""type"":""bool""}],""stateMutability"":""view"",""type"":""function""},{""inputs"":[],""name"":""name"",""outputs"":[{""internalType"":""string"",""name"":"""",""type"":""string""}],""stateMutability"":""view"",""type"":""function""},{""inputs"":[{""internalType"":""uint256"",""name"":""tokenId"",""type"":""uint256""}],""name"":""ownerOf"",""outputs"":[{""internalType"":""address"",""name"":"""",""type"":""address""}],""stateMutability"":""view"",""type"":""function""},{""inputs"":[{""internalType"":""address"",""name"":""to"",""type"":""address""}],""name"":""safeMint"",""outputs"":[],""stateMutability"":""nonpayable"",""type"":""function""},{""inputs"":[{""internalType"":""address"",""name"":""from"",""type"":""address""},{""internalType"":""address"",""name"":""to"",""type"":""address""},{""internalType"":""uint256"",""name"":""tokenId"",""type"":""uint256""}],""name"":""safeTransferFrom"",""outputs"":[],""stateMutability"":""nonpayable"",""type"":""function""},{""inputs"":[{""internalType"":""address"",""name"":""from"",""type"":""address""},{""internalType"":""address"",""name"":""to"",""type"":""address""},{""internalType"":""uint256"",""name"":""tokenId"",""type"":""uint256""},{""internalType"":""bytes"",""name"":""data"",""type"":""bytes""}],""name"":""safeTransferFrom"",""outputs"":[],""stateMutability"":""nonpayable"",""type"":""function""},{""inputs"":[{""internalType"":""address"",""name"":""operator"",""type"":""address""},{""internalType"":""bool"",""name"":""approved"",""type"":""bool""}],""name"":""setApprovalForAll"",""outputs"":[],""stateMutability"":""nonpayable"",""type"":""function""},{""inputs"":[{""internalType"":""bytes4"",""name"":""interfaceId"",""type"":""bytes4""}],""name"":""supportsInterface"",""outputs"":[{""internalType"":""bool"",""name"":"""",""type"":""bool""}],""stateMutability"":""view"",""type"":""function""},{""inputs"":[],""name"":""symbol"",""outputs"":[{""internalType"":""string"",""name"":"""",""type"":""string""}],""stateMutability"":""view"",""type"":""function""},{""inputs"":[{""internalType"":""uint256"",""name"":""tokenId"",""type"":""uint256""}],""name"":""tokenURI"",""outputs"":[{""internalType"":""string"",""name"":"""",""type"":""string""}],""stateMutability"":""view"",""type"":""function""},{""inputs"":[{""internalType"":""address"",""name"":""from"",""type"":""address""},{""internalType"":""address"",""name"":""to"",""type"":""address""},{""internalType"":""uint256"",""name"":""tokenId"",""type"":""uint256""}],""name"":""transferFrom"",""outputs"":[],""stateMutability"":""nonpayable"",""type"":""function""}]";


    var contract = web3.Eth.GetContract(abi, toAddress);
    var mintFunction = contract.GetFunction("safeMint");

    Console.WriteLine("Minting ....");

    var gas = new HexBigInteger(Web3.Convert.ToWei(gasPriceGwei, UnitConversion.EthUnit.Gwei));
    // argument : from, gasLimit, value, cancellation token, toAddress (safemint function argument)
    var mintedNft = await mintFunction.SendTransactionAndWaitForReceiptAsync(externalAccount.Address, new HexBigInteger(gasLimit), null, null, externalAccount.Address);

    if (mintedNft.Succeeded())
    {
        Console.WriteLine($"Nft created Transaction Id: {mintedNft.TransactionHash}");
    }
    else
    {
        Console.WriteLine($"Nft creation failed Transaction Id: {mintedNft.TransactionHash}");
    }

}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
