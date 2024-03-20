using System;
using System.Collections.Generic;
using System.Text;

namespace StartdustCustodialSDK.Signers
{
    public class ApiRequestPayload
    {
        public string WalletId { get; set; }
        public ChainType ChainType { get; set; }
    }
}
