using System;
using System.Collections.Generic;
using System.Text;

namespace StartdustCustodialSDK.Profile
{
    public class StartdustProfileIdentifierListResult
    {
        public List<StardustProfileIdentifier> Results { get; set; }
        public int Total { get; set; }
        public Filter Filter { get; set; }
        public int Start { get; set; }
        public int Limit { get; set; }
    }

    public class Filter
    {
        public string ProfileId { get; set; }
    }
}
