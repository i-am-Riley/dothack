namespace Rileysoft.DotHack.Extensions
{
    internal static class StreamExtensions
    {
        internal static uint ReadUnsignedInt (this Stream stream)
        {
            byte[] bytes = new byte[4];
            int bytesRead = stream.Read(bytes, 0, 4);

            if (bytesRead < 4)
                throw new EndOfStreamException("end of stream");

            return bytes.ReadUnsignedInt();
        }

        internal static ushort ReadUnsignedShort (this Stream stream)
        {
            byte[] bytes = new byte[2];
            int bytesRead = stream.Read(bytes, 0, 2);

            if (bytesRead < 2)
                throw new EndOfStreamException("end of stream");

            return bytes.ReadUnsignedShort();
        }

        internal static int ReadInt (this Stream stream)
        {
            byte[] bytes = new byte[4];
            int bytesRead = stream.Read(bytes, 0, 4);

            if (bytesRead < 4)
                throw new EndOfStreamException("end of stream");

            return bytes.ReadInt();
        }

        internal static int ReadShort(this Stream stream)
        {
            byte[] bytes = new byte[2];
            int bytesRead = stream.Read(bytes, 0, 2);

            if (bytesRead < 2)
                throw new EndOfStreamException("end of stream");

            return bytes.ReadShort();
        }

        internal static string ReadCString(this Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

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
