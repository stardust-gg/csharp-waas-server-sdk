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
        public async void CreateAProfile()
        {
            // Instruction in the README file to test this part
            if (!string.IsNullOrEmpty(apiKey))
            {
                var stardustApplication = new StardustApplicationAPI(apiKey);
                var myApp = await stardustApplication.Get();

                var stardustProfileApi = new StardustProfileAPI(apiKey);
                var profileParam = new StardustProfileCreateParams(myApp.Id, "test");
                var profile = await stardustProfileApi.Create(profileParam);
                Assert.NotNull(profile);
                output.WriteLine($"Id : {profile.Id}");
                output.WriteLine($"Name : {profile.Name}");
            }
        }

    }
}