using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace StartdustCustodialSDK.Profile
{
    // Todo verify what type of data come for this enum
    public enum StardustProfileIdentifierService
    {
        [Description(":external-wallet")]
        ExternalWallet,
        [Description("discord")]
        Discord,
        [Description("apple")]
        Apple,
        [Description("google")]
        Google,
        [Description("facebook")]
        Facebook,
        [Description("twitter")]
        Twitter,
        [Description("email")]
        Email,
        [Description("phone")]
        Phone
    }
}
