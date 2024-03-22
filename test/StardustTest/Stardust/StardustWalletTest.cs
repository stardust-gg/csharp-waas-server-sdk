using StardustTest.Config;
using StartdustCustodialSDK.Application;
using StartdustCustodialSDK.Profile;
using System.Diagnostics;
using Xunit.Abstractions;
using StartdustCustodialSDK.Utils;
using StartdustCustodialSDK.Wallet;
namespace StardustTest.Stardust
{
    public class StardustWalletTest
    {
        private readonly ITestOutputHelper output;
        string apiKey;

        public StardustWalletTest(ITestOutputHelper output)
        {
            var config = TestConfigHelper.GetIConfigurationRoot();
            this.output = output;
            apiKey = config["ApiKey"];
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
                var walletId = profile.Wallet.Id;
                // get it from wallet to test if value are the same
                var wallet = new StardustWallet(walletId, profile.Id, myApp, apiKey);
                var getProfile = await wallet.GetProfile();
                Assert.Equal(profile.Id, getProfile.Id);
                Assert.Equal(profile.Name, getProfile.Name);
                Assert.Equal(profile.Wallet.Id, getProfile.Wallet.Id);
            }
        }
    }
}