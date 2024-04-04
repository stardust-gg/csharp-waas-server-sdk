# csharp-waas-server-sdk

Created with Visual Studio 2022

## Installation

[Download and install the .net sdk 8.0](https://dotnet.microsoft.com/en-us/download/visual-studio-sdks)

To open the project you can use :
- Visual Studio 2022 on Windows
- Rider Ide 
- Visual Studio Code

[The c# dev kit if you want to launch the test on VS Code](https://code.visualstudio.com/docs/csharp/testing)

Clone the repository 
```
git clone https://github.com/stardust-gg/csharp-waas-server-sdk.git
cd csharp-waas-server-sdk
``` 

[More info about the cli interface](https://learn.microsoft.com/en-us/dotnet/core/tools/)


## StardustCustodialSDK

NetStandard 2.0 library for maximum compatibility.

You just need to reference the project to use it or build the dll and import it in your c# project.

Build with cli 
```
dotnet build .\src\StartdustCustodialSDK\StartdustCustodialSDK.csproj --configuration Release
```

[More info about the build command](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-build)

## StardustTest

Net 8.0 Test project.

To test from Visual Studio, create a file appsettings.local.json in StardustTest and put your apiKey in it if you want to test Api calls.

Add a wallet Id with some matic on mumbai to test transaction with the nethereum signer.

```
{
  "ApiKey": "c80****-****-****-****-********5a",  
  "WalletId": "ea5****-****-****-****-********4f"
}
```

To launch the test from Cli
```
dotnet test -e --logger "console;verbosity=detailed"
``` 

Define ApiKey to test webservice call 
```
dotnet test -e ApiKey="MyApiKey" --logger "console;verbosity=detailed"
``` 

Define a Wallet Id with some matic on mumbai to test nethereum transfer
```
dotnet test -e ApiKey="MyApiKey" -e WalletId="MyWalletId" --logger "console;verbosity=detailed"
``` 

[More info about the test command](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-test)

## WebApi Example

Asp.Net Core 8.0 Api project with a swagger interface.

Example of sdk use in api web controllers.

Launch the WebApiExample in cli.

```
dotnet run --project .\examples\WebApiExample\WebApiExample.csproj
``` 

And open your bowser on [http://localhost:5079/index.html](http://localhost:5079/index.html)

## Common Usage

You can see examples in StardustTest and WebApiExample.

### Get app information

```cs
string apiKey = "myApiKey";
var stardustApplication = new StardustApplicationAPI(apiKey);
var myApp = await stardustApplication.Get();
```

### Profile management

Create a profile
```cs
string apiKey = "myApiKey";
var stardustApplication = new StardustApplicationAPI(apiKey);
var myApp = await stardustApplication.Get();

string profileName = "My new profile";
var stardustProfileApi = new StardustProfileAPI(apiKey);
var profileParam = new StardustProfileCreateParams(myApp.Id, profileName);
var profile = await stardustProfileApi.Create(profileParam); 
```

Get a profile by Id
```cs
string apiKey = "myApiKey";
string profileId = "profileId";
var stardustProfileApi = new StardustProfileAPI(apiKey);
var getProfile = await stardustProfileApi.Get(profileId);
```


Create a profile Identifier
```cs
string apiKey = "myApiKey";
string profileId = "profileId";
var stardustProfileApi = new StardustProfileAPI(apiKey);
var getProfile = await stardustProfileApi.Get(profileId);
var newIdentifier = await getProfile.AddIdentifier(StardustProfileIdentifierService.Twitter, "IdentifierValue");
```

Create a custom profile Identifier
```cs
string apiKey = "myApiKey";
string profileId = "profileId";
var stardustProfileApi = new StardustProfileAPI(apiKey);
var getProfile = await stardustProfileApi.Get(profileId);
var newIdentifier = await getProfile.AddCustomIdentifier("IdentifierService", "IdentifierValue");
```


Get profile Identifiers
```cs
string apiKey = "myApiKey";
string profileId = "profileId";
var stardustProfileApi = new StardustProfileAPI(apiKey);
var getProfile = await stardustProfileApi.Get(profileId);
var getListIdentifier = await getProfile.GetIdentifiers();
```

### Wallet 

Get Wallet
```cs
string apiKey = "myApiKey";
string profileId = "profileId";
var stardustProfileApi = new StardustProfileAPI(apiKey);
var getProfile = await stardustProfileApi.Get(profileId);
var wallet = await getProfile.Wallet;
```

Get profile from a Wallet
```cs
string apiKey = "myApiKey";
string profileId = "profileId";
string walletId = "walletId";
var stardustApplication = new StardustApplicationAPI(apiKey);
var myApp = await stardustApplication.Get();

var wallet = new StardustWallet(walletId, profileId, myApp, apiKey);
var getProfile = await wallet.GetProfile();
```

### Nethereum

You can use Nethereum with our custom EthExternalSignerBase

Send some native token to an user (Ethereum, Matic, BNB ...)
```cs
string apiKey = "myApiKey";
string walletId = "walletId";
// Example for send matic in Mumbai testnet
string rpcAddress = "https://rpc.ankr.com/polygon_mumbai";
var rpcClient = new RpcClient(new Uri(rpcAddress));

// use walletId with token to test this part (chain id 80001 or 0x13881 for mumbai)
var nethereumSigner = new NethereumStardustSigner(apiKey, walletId, "0x13881");
var externalAccount = new ExternalAccount(nethereumSigner);
await externalAccount.InitialiseAsync();
externalAccount.InitialiseDefaultTransactionManager(rpcClient);

// Initialize Web3
var web3 = new Web3(externalAccount, rpcClient);
web3.TransactionManager.UseLegacyAsDefault = true;

var toAddress = "0x0f571D2625b503BB7C1d2b5655b483a2Fa696fEf"; // Replace with the address of the recipient
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
var transaction = await web3.Eth.Transactions.SendRawTransaction.SendRequestAsync(signedTransaction);
```

Get Wallet Address
```cs
string apiKey = "myApiKey";
string walletId = "walletId";
var nethereumSigner = new NethereumStardustSigner(apiKey, walletId);
var getAddress = await nethereumSigner.GetAddressAsync();
```

[Nethereum documentation](https://docs.nethereum.com/en/latest/)

### Evm Signer

You can sign message with this library, also Evm Signer is available in NethereumStardustSigner

Sign raw message (string and byte[] supported)
```cs
string apiKey = "myApiKey";
string walletId = "walletId";
var evmSigner = new EvmStardustSigner(apiKey, walletId);
var msg1 = "Hello world message 18/09/2017 02:55PM";
var result = await evmSigner.SignRaw(msg1)
```

Prefix and sign message with [eip-191](https://eips.ethereum.org/EIPS/eip-191) (string and byte[] supported)
```cs
string apiKey = "myApiKey";
string walletId = "walletId";
var evmSigner = new EvmStardustSigner(apiKey, walletId);
var msg1 = "Hello world message 18/09/2017 02:55PM
var result = await evmSigner.SignMessage(msg1);
```

Get account Address
```cs
string apiKey = "myApiKey";
string walletId = "walletId";
var evmSigner = new EvmStardustSigner(apiKey, walletId);
var getAddress = await evmSigner.GetAddress();
```

Get account Public Key
```cs
string apiKey = "myApiKey";
string walletId = "walletId";
var evmSigner = new EvmStardustSigner(apiKey, walletId);
var publicKey = await evmSigner.GetPublicKey();
```