using System;
using System.Collections.Generic;
using System.Text;

namespace StartdustCustodialSDK.Profile
{
    public class StardustProfileIdentifierCreateParams
    {
        public string ProfileId { get; set; }
        public string Service { get; set; }
        public string Value { get; set; }

        public StardustProfileIdentifierCreateParams() { }

        public StardustProfileIdentifierCreateParams(string profileId, string service, string value)
        {
            ProfileId = profileId;
            Service = service;
            Value = value;
        }
    }
}
