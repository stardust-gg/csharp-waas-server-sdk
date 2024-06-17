using Nethereum.Hex.HexConvertors.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace StartdustCustodialSDK.Utils
{
    public static class HexUtils
    {
        /// <summary>
        /// Convert string or hex string to byte array
        /// </summary>
        /// <param name="message">string to convert</param>
        /// <returns>byte array</returns>
        public static byte[] ToByte(this string message)
        {
            if (message.IsHex())
            {
                return message.HexToByteArray();
            }
            else
            {
                return Encoding.UTF8.GetBytes(message);
            }
        }
    }
}
