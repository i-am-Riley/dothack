using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rileysoft.DotHack.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Converts "8A 3F" to byte[] { 0x8A, 0x3F }
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="OverflowException"></exception>
        public static byte[] ToByteArrayFromHex (this string str)
        {
            if (str == null)
                throw new ArgumentNullException (nameof (str));

            string[] parts = str.Split(' ');
            byte[] bytes = new byte[parts.Length];

            for (int i = 0; i < parts.Length; i++)
            {
                bytes[i] = byte.Parse(parts[i], System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture);
            }

            return bytes;
        }
    }
}
