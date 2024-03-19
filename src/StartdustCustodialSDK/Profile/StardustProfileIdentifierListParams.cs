using System;
using System.Collections.Generic;
using System.Text;

namespace StartdustCustodialSDK.Profile
{
    public class StardustProfileIdentifierListParams
    {
        public string ProfileId { get; set; }
        public int Start { get; set; }
        public int Limit { get; set; }

        public StardustProfileIdentifierListParams() { }

        public StardustProfileIdentifierListParams(string profileId, int start, int limit)
        {
            ProfileId = profileId;
            Start = start;
            Limit = limit;
        }   
    }
}
