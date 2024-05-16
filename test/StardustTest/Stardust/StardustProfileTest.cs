using StardustTest.Config;
using StartdustCustodialSDK.Application;
using StartdustCustodialSDK.Profile;
using System.Diagnostics;
using Xunit.Abstractions;
using StartdustCustodialSDK.Utils;
using Nethereum.Contracts.QueryHandlers.MultiCall;
namespace StardustTest.Stardust
{
    public class StardustProfileTest
    {
        private readonly ITestOutputHelper output;
        string apiKey;

        public StardustProfileTest(ITestOutputHelper output)
        {
            var config = TestConfigHelper.GetIConfigurationRoot();
            this.output = output;
            apiKey = config["ApiKey"];
        }

        [Fact]
        public async void CreateProfile()
        {
            // Instruction in the README file to test this part
            if (!string.IsNullOrEmpty(apiKey))
            {
                var stardustApplication = new StardustApplicationAPI(apiKey);
                var myApp = await stardustApplication.Get();

                var stardustProfileApi = new StardustProfileAPI(apiKey);
                var profileParam = new StardustProfileCreateParams(myApp.Id, "test create profile");
                var profile = await stardustProfileApi.Create(profileParam);
                Assert.NotNull(profile);
                Assert.NotNull(profile.Id);
                output.WriteLine($"Profile Id : {profile.Id}");
                output.WriteLine($"Profile Name : {profile.Name}");
            }
        }

        [Fact]
        public async void GetProfile()
        {
            // Instruction in the README file to test this part
            if (!string.IsNullOrEmpty(apiKey))
            {
                var stardustApplication = new StardustApplicationAPI(apiKey);
                var myApp = await stardustApplication.Get();

                var stardustProfileApi = new StardustProfileAPI(apiKey);
                var profileParam = new StardustProfileCreateParams(myApp.Id, "test get profile");
                // create profile
                var profile = await stardustProfileApi.Create(profileParam);
                // get it to test if value are the same
                var getProfile = await stardustProfileApi.Get(profile.Id);
                Assert.Equal(profile.Id, getProfile.Id);
                Assert.Equal(profile.Name, getProfile.Name);
                Assert.Equal(profile.Wallet.Id, getProfile.Wallet.Id);
                output.WriteLine($"Profile Id : {profile.Id}");
                output.WriteLine($"Profile Name : {profile.Name}");
            }
        }

        [Fact]
        public async void GetProfileToken()
        {
            // Instruction in the README file to test this part
            if (!string.IsNullOrEmpty(apiKey))
            {
                var stardustApplication = new StardustApplicationAPI(apiKey);
                var myApp = await stardustApplication.Get();

                var stardustProfileApi = new StardustProfileAPI(apiKey);
                var profileParam = new StardustProfileCreateParams(myApp.Id, "test profile token");
                // create profile
                var profile = await stardustProfileApi.Create(profileParam);
                // get token
                var token = await stardustProfileApi.GenerateClientJWT(profile.Id, 1000);
                Assert.NotNull(token);
                output.WriteLine($"Token : {token}");
            }
        }


        [Fact]
        public async void CreateProfileIdentifier()
        {
            // Instruction in the README file to test this part
            if (!string.IsNullOrEmpty(apiKey))
            {
                var stardustApplication = new StardustApplicationAPI(apiKey);
                var myApp = await stardustApplication.Get();

                var stardustProfileApi = new StardustProfileAPI(apiKey);
                var profileParam = new StardustProfileCreateParams(myApp.Id, "test profile identifier");
                // create profile
                var profile = await stardustProfileApi.Create(profileParam);
                var newIdentifier = await profile.AddIdentifier(StardustProfileIdentifierService.Twitter, "test identifier");
                Assert.NotNull(newIdentifier);
                Assert.Equal(newIdentifier.Service, StardustProfileIdentifierService.Twitter.DisplayName());
                Assert.Equal(newIdentifier.Value, "test identifier");
                output.WriteLine($"Profile Id : {profile.Id}");
                output.WriteLine($"Profile Name : {profile.Name}");
                output.WriteLine($"Profile Identifier : {newIdentifier.Service}");
                output.WriteLine($"Profile Identifier Value : {newIdentifier.Value}");

                await profile.AddCustomIdentifier("Metamask", "test identifier 2");

                // find all created identifier for this profile
                var getListIdentifier = await profile.GetIdentifiers();
                Assert.Equal(getListIdentifier.Count, 2);
                Assert.Contains(getListIdentifier, x => x.Service == $":Metamask");

            }
        }
    }
}