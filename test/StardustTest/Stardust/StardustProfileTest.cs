using StardustTest.Config;
using StartdustCustodialSDK.Application;
using StartdustCustodialSDK.Profile;
using System.Diagnostics;
using Xunit.Abstractions;

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
                output.WriteLine($"Id : {profile.Id}");
                output.WriteLine($"Name : {profile.Name}");
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

    }
}