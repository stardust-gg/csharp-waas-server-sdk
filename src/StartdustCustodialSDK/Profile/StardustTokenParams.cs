using System;
using System.Collections.Generic;
using System.Text;

namespace StartdustCustodialSDK.Profile
{
    public class StardustTokenParams
    {
        public long Duration { get; set; }

        public StardustTokenParams()
        {
        }

        public StardustTokenParams(long duration)
        {
            this.Duration = duration;
        }

    }
}
