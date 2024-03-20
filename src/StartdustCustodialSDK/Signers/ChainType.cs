using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace StartdustCustodialSDK.Signers
{
    public enum ChainType
    {
        [Description("evm")]
        Evm,
        [Description("sui")]
        Sui,
        [Description("sol")]
        Sol

    }
}
