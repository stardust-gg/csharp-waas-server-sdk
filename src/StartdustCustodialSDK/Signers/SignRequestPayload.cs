using StartdustCustodialSDK.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartdustCustodialSDK.Signers
{
    public class SignRequestPayload<T> : ApiRequestPayload
    {
        public string ChainId { get; set; }
        /// <summary>
        /// T is string or bytes
        /// </summary>
        public T Message { get; set; }

        public SignRequestPayload() { }

        public SignRequestPayload(string walletId, ChainType chainType, string chainId, T message)
        {

            WalletId = walletId;
            ChainType = chainType.DisplayName();
            ChainId = chainId;
            Message = message;
        }
    }
}
