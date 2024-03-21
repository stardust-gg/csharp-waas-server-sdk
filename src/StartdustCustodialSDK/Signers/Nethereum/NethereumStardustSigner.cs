using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StartdustCustodialSDK.Signers.Nethereum
{
    public class NethereumStardustSigner : AbstractStardustSigner
    {
        public override Task<string> GetAddress()
        {
            throw new NotImplementedException();
        }

        public override Task<string> GetPublicKey()
        {
            throw new NotImplementedException();
        }

        public override Task<string> SignRaw<T>(T message)
        {
            throw new NotImplementedException();
        }
    }
}
