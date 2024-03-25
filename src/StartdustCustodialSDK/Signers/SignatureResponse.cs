using System;
using System.Collections.Generic;
using System.Text;

namespace StartdustCustodialSDK.Signers
{
    public class SignatureResponse
    {
        public string Signature { get; set; }
        public string WalletId { get; set; }
        public string ChainType { get; set; }
        public string ChainId { get; set; }
        public string Message { get; set; }
    }
}
