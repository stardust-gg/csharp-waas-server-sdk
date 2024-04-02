using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StartdustCustodialSDK.Application;
using StartdustCustodialSDK.Profile;

namespace WebsiteExample.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {

        [HttpGet(Name = "GetProfile")]
        public async Task<IActionResult> GetProfile([FromQuery] string apiKey, string profileId)
        {
            var stardustProfileApi = new StardustProfileAPI(apiKey);
            // get it to test if value are the same
            var getProfile = await stardustProfileApi.Get(profileId);
            return Ok(getProfile);
        }

        [HttpPost(Name = "AddProfile")]
        public async Task<IActionResult> AddProfile(ProfileData profileData)
        {

            var stardustApplication = new StardustApplicationAPI(profileData.ApiKey);
            var myApp = await stardustApplication.Get();

            var stardustProfileApi = new StardustProfileAPI(profileData.ApiKey);
            var profileParam = new StardustProfileCreateParams(myApp.Id, profileData.Name);
            var profile = await stardustProfileApi.Create(profileParam); 
            return Ok(profile);
        }
    }

    public class ProfileData
    {
        public string ApiKey { get; set; }
        public string Name { get; set; }
    }
}
