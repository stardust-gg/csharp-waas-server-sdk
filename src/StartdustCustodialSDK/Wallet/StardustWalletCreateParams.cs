using System;
using System.Collections.Generic;
using System.Text;

namespace StartdustCustodialSDK.Wallet
{
    public class StardustWalletCreateParams
    {
        public string ProfileId { get; set; }

        public StardustWalletCreateParams()
        {

        }

        public StardustWalletCreateParams(string profileId)
        {
            ProfileId = profileId;
        }
    }
}
