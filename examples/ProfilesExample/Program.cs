// See https://aka.ms/new-console-template for more information
using StartdustCustodialSDK;
using System.Text.Json;

Console.WriteLine("Create profile");

try
{
    var apiKey = args[0];

    var stardustCustodialSdk = new StardustCustodialSdk(apiKey);
    var application = await stardustCustodialSdk.GetApplication();
    var profile = await stardustCustodialSdk.CreateProfile(application.Id, "My profile Name");
    Console.WriteLine($"Profile created : {JsonSerializer.Serialize(profile)}");
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
