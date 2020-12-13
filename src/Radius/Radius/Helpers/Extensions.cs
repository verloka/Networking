using System;

namespace Radius.Helpers
{
    public static class Extensions
    {
        /// <summary>
        /// Convert a string of hex encoded bytes to a byte array
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static byte[] StringToByteArray(string hex)
        {
            var NumberChars = hex.Length;
            var bytes = new byte[NumberChars / 2];
            for (var i = 0; i < NumberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }

        /// <summary>
        /// Convert a byte array to a string of hex encoded bytes
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ToHexString(this byte[] bytes)
        {
            return bytes != null ? BitConverter.ToString(bytes).ToLowerInvariant().Replace("-", "") : null;
        }
    }
}
