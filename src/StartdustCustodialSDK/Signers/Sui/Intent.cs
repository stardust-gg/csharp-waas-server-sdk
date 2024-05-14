using System;
using System.Collections.Generic;
using System.Text;

namespace StartdustCustodialSDK.Signers.Sui
{
    public enum IntentScope : byte
    {
        TransactionData = 0,
        TransactionEffects = 1,
        AuthorityBatch = 2,
        CheckpointSummary = 3,
        PersonalMessage = 4,
    }

    public enum AppId : byte
    {
        Sui = 0,
    }

    public enum IntentVersion : byte
    {
        V0 = 0,
    }

    public static class Intent
    {
        public static byte[] IntentWithScope(IntentScope scope)
        {
            return new byte[] { (byte)scope, (byte)IntentVersion.V0, (byte)AppId.Sui };
        }

        public static byte[] MessageWithIntent(IntentScope scope, IEnumerable<byte> message)
        {
            var intent = IntentWithScope(scope);
            var intentMessage = new List<byte>();
            intentMessage.AddRange(intent);
            intentMessage.AddRange(message);
            return intentMessage.ToArray();
        }
    }
}
