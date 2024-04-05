// See https://aka.ms/new-console-template for more information
using StartdustCustodialSDK;

Console.WriteLine("Create profile JWT");

try
{
    var apiKey = args[0];
    var profileId = args[1];

    var stardustCustodialSdk = new StardustCustodialSdk(apiKey);
    var jwt = await stardustCustodialSdk.GenerateProfileJWT(profileId, 3600);
    Console.WriteLine($"Profile JWT created : {jwt}");
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}