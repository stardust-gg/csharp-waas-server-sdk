using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StartdustCustodialSDK.Signers
{
    public abstract class AbstractStardustSigner
    {
        public abstract Task<string> GetPublicKey();
        public abstract Task<string> GetAddress();
        public abstract Task<string> SignRaw<T>(T message);
    }
}
