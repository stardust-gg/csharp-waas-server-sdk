using StartdustCustodialSDK.Application;

namespace StardustTest.Stardust
{
    public class StardustAppTest
    {
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
    }
}