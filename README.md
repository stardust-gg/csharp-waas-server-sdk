# csharp-waas-server-sdk

Created with Visual Studio 2022

## Installation

[Download and install the .net sdk 8.0](https://dotnet.microsoft.com/en-us/download/visual-studio-sdks)

To open the project you can use :
- Visual Studio 2022 on Windows
- Rider Ide 
- Visual Studio Code

[The c# dev kit if you want to launch the test on VS Code](https://code.visualstudio.com/docs/csharp/testing)

## StardustCustodialSDK

NetStandard 2.0 library for maximum compatibility.

You just need to reference the project to use it or build the dll and import it in your c# project.

## StardustTest

Net 8.0 Test project.

Create a file appsettings.local.json in StardustTest and put your apiKey in it if you want to test Api calls.

Add a wallet Id with some matic on mumbai to test transaction with the nethereum signer.

```
{
  "ApiKey": "c80****-****-****-****-********5a",  
  "WalletId": "ea5****-****-****-****-********4f"
}
```
