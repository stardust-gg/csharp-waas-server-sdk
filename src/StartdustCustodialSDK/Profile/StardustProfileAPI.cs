using System;
using System.Collections.Generic;
using System.Text;

namespace StartdustCustodialSDK.Profile
{
    public class StardustProfileAPI : BaseStardustAPI
    {
        public StardustProfileAPI(string apiKey) : base(apiKey)
        {
        }

        public StardustProfileAPI(string apiKey, string url) : base(apiKey, url)
        {
        }

    }
}
