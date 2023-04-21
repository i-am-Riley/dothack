using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rileysoft.DotHack.Extensions
{
    public static class StreamExtensions
    {
        public static string ReadCString (this Stream stream)
        {
            char[] charBuf = new char[1];
            byte[] readBuf = new byte[1];
            int len = 0;

            while (stream.Read(readBuf, 0, 1) == 1)
            {
                if (readBuf[0] == 0)
                    break;

                if (len == charBuf.Length)
                {
                    char[] cloneBuf = new char[charBuf.Length * 2];
                    for (int i = 0; i < len; i++)
                    {
                        cloneBuf[i] = charBuf[i];
                    }
                    charBuf = cloneBuf;
                }

                charBuf[len++] = Convert.ToChar(readBuf[0]);
            }

            return new string(charBuf, 0, len);
        }
    }
}
