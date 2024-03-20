using System;
using System.Collections.Generic;
using System.Text;

namespace StartdustCustodialSDK.Signers
{
    public class SignRequestPayload<T> : ApiRequestPayload
    {
        /// <summary>
        /// T is string or bytes
        /// </summary>
        public T Message { get; set; }
    }
}
