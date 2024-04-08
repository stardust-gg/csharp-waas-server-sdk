using StardustTest.Config;
using StartdustCustodialSDK;
using StartdustCustodialSDK.Application;
using System.Diagnostics;
using Xunit.Abstractions;

namespace StardustTest.Stardust
{
    public class StardustSDKTest
    {
        private readonly ITestOutputHelper output;
        string apiKey;

        public StardustSDKTest(ITestOutputHelper output)
        {
            var config = TestConfigHelper.GetIConfigurationRoot();
            this.output = output;
            apiKey = config["ApiKey"];
        }



        [Fact]
        public async void CreateWallet()
        {
            // Instruction in the README file to test this part
            if (!string.IsNullOrEmpty(apiKey))
            {
                // test creation of new wallet from sdk class
                var stardustCustodialSdk = new StardustCustodialSdk(apiKey);
                var newWallet = await stardustCustodialSdk.CreateWallet();
                Assert.NotNull(newWallet);
                Assert.NotNull(newWallet.Id);
                Assert.NotNull(newWallet.ProfileId);
                output.WriteLine($"Wallet Id : {newWallet.Id}");

                // test if we can retrieve this new wallet
                var getWallet = await stardustCustodialSdk.GetWallet(newWallet.Id);
                Assert.Equal(newWallet.Id, getWallet.Id);
                Assert.Equal(newWallet.ProfileId, getWallet.ProfileId);
                output.WriteLine($"Profile Id : {getWallet.ProfileId}");
            }
        }

        [Fact]
        public async void CreateProfile()
        {
            // Instruction in the README file to test this part
            if (!string.IsNullOrEmpty(apiKey))
            {
                // test creation of new profile from sdk class
                var stardustCustodialSdk = new StardustCustodialSdk(apiKey);
                var app = await stardustCustodialSdk.GetApplication();
                var newProfile = await stardustCustodialSdk.CreateProfile(app.Id);
                Assert.NotNull(newProfile);
                Assert.NotNull(newProfile.Id);
                output.WriteLine($"Profile Id : {newProfile.Id}");

                // test if we can retrieve this new profile
                var getProfile = await stardustCustodialSdk.GetProfile(newProfile.Id);
                Assert.Equal(newProfile.Id, getProfile.Id);
                output.WriteLine($"Retrieve Profile Id : {getProfile.Id}");
            }
        }
    }
}