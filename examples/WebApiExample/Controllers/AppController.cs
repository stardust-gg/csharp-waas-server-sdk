using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StartdustCustodialSDK.Application;

namespace WebsiteExample.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppController : ControllerBase
    {
        public AppController()
        {
          
        }

        [HttpGet(Name = "GetApp")]
        public async Task<IActionResult> GetApp([FromQuery]string apiKey)
        {
            var stardustApplication = new StardustApplicationAPI(apiKey);
            var myApp = await stardustApplication.Get();
            return Ok(myApp);
        }
    }
}
