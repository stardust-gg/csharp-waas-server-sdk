using StardustTest.Config;
using StartdustCustodialSDK.Application;
using System.Diagnostics;
using Xunit.Abstractions;

namespace StardustTest.Stardust
{
    public class StardustAppTest
    {
        private readonly ITestOutputHelper output;
        string apiKey;

        public StardustAppTest(ITestOutputHelper output)
        {
            var config = TestConfigHelper.GetIConfigurationRoot();
            this.output = output;
            apiKey = config["ApiKey"];
        }

        [Fact]
        public void CreateAnApp()
        {
            var app = new StardustApplication("id-here", "name", "email", "description");
            Assert.NotNull(app);
            Assert.Equal("name", app.Name);
            Assert.Equal("email", app.Email);
            Assert.Equal("description", app.Description);
            Assert.Equal("id-here", app.Id);
            Assert.Null(app.ApiKey);
        }

        [Fact]
        public async void GetMyApp()
        {
            // Instruction in the README file to test this part
            if (!string.IsNullOrEmpty(apiKey))
            {
                var stardustApplication = new StardustApplicationAPI(apiKey);
                var myApp = await stardustApplication.Get();
                Assert.NotNull(myApp);
                Assert.NotNull(myApp.Name);
                output.WriteLine($"App Id : {myApp.Id}");
                output.WriteLine($"App Name : {myApp.Name}");
            }
        }
    }
}