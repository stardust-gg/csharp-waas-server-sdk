using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace StartdustCustodialSDK.Profile
{
    // Todo verify what type of data come for this enum
    public enum StardustProfileIdentifierService
    {
        [Description("csharp-sdk:external-wallet")]
        ExternalWallet,
        [Description("csharp-sdk:discord")]
        Discord,
        [Description("csharp-sdk:apple")]
        Apple,
        [Description("csharp-sdk:google")]
        Google,
        [Description("csharp-sdk:facebook")]
        Facebook,
        [Description("csharp-sdk:twitter")]
        Twitter,
        [Description("csharp-sdk:email")]
        Email,
        [Description("csharp-sdk:phone")]
        Phone
    }
}
