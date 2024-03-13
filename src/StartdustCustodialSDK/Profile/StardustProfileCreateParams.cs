using System;
using System.Collections.Generic;
using System.Text;

namespace StartdustCustodialSDK.Profile
{
    public class StardustProfileCreateParams
    {
        public string ApplicationId { get; set; }
        public string Name { get; set; }

        public StardustProfileCreateParams() { }

        public StardustProfileCreateParams(string applicationId, string name)
        {
            ApplicationId = applicationId;
            Name = name;
        }
    }
}
