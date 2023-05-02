namespace Rileysoft.DotHack.Extensions
{
    public static class StreamExtensions
    {
        public static uint ReadUnsignedIntLE (this Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            byte[] bytes = new byte[4];
            int bytesRead = stream.Read(bytes, 0, 4);

            if (bytesRead < 4)
                throw new EndOfStreamException("end of stream");

            return bytes.ReadUnsignedIntLE();
        }

        public static ushort ReadUnsignedShortLE (this Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            byte[] bytes = new byte[2];
            int bytesRead = stream.Read(bytes, 0, 2);

            if (bytesRead < 2)
                throw new EndOfStreamException("end of stream");

            return bytes.ReadUnsignedShortLE();
        }

        public static int ReadIntLE (this Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            byte[] bytes = new byte[4];
            int bytesRead = stream.Read(bytes, 0, 4);

            if (bytesRead < 4)
                throw new EndOfStreamException("end of stream");

            return bytes.ReadIntLE();
        }


        public static short ReadShortLE(this Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            byte[] bytes = new byte[2];
            int bytesRead = stream.Read(bytes, 0, 2);

            if (bytesRead < 2)
                throw new EndOfStreamException("end of stream");

            return bytes.ReadShortLE();
        }

        public static IntPtr ReadIntPtrLE(this Stream stream)
        {
            return new IntPtr(ReadIntLE(stream));
        }

        public static UIntPtr ReadUIntPtrLE(this Stream stream)
        {
            return new UIntPtr(ReadUnsignedIntLE(stream));
        }

        public static uint ReadUnsignedIntBE(this Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            byte[] bytes = new byte[4];
            int bytesRead = stream.Read(bytes, 0, 4);

            if (bytesRead < 4)
                throw new EndOfStreamException("end of stream");

            return bytes.ReadUnsignedIntBE();
        }

        public static ulong ReadUnsignedLongBE(this Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            byte[] bytes = new byte[8];
            int bytesRead = stream.Read(bytes, 0, 8);

            if (bytesRead < 8)
                throw new EndOfStreamException("end of stream");

            return bytes.ReadUnsignedLongBE();
        }

        public static ulong ReadUnsignedLongLE(this Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            byte[] bytes = new byte[8];
            int bytesRead = stream.Read(bytes, 0, 8);

            if (bytesRead < 8)
                throw new EndOfStreamException("end of stream");

            return bytes.ReadUnsignedLongLE();
        }



        public static ushort ReadUnsignedShortBE(this Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            byte[] bytes = new byte[2];
            int bytesRead = stream.Read(bytes, 0, 2);

            if (bytesRead < 2)
                throw new EndOfStreamException("end of stream");

            return bytes.ReadUnsignedShortBE();
        }

        public static int ReadIntBE(this Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            byte[] bytes = new byte[4];
            int bytesRead = stream.Read(bytes, 0, 4);

            if (bytesRead < 4)
                throw new EndOfStreamException("end of stream");

            return bytes.ReadIntBE();
        }

        public static short ReadShortBE(this Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            byte[] bytes = new byte[2];
            int bytesRead = stream.Read(bytes, 0, 2);

            if (bytesRead < 2)
                throw new EndOfStreamException("end of stream");

            return bytes.ReadShortBE();
        }

        public static IntPtr ReadIntPtrBE(this Stream stream)
        {
            return new IntPtr(ReadIntBE(stream));
        }

        public static UIntPtr ReadUIntPtrBE(this Stream stream)
        {
            return new UIntPtr(ReadUnsignedIntBE(stream));
        }

        public static List<string> ReadCStrings(this Stream stream, long offset, long length)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            if (!stream.CanSeek)
                throw new ArgumentException("cannot seek");

            byte[] buffer = new byte[length];
            stream.Seek(offset, SeekOrigin.Begin);
            stream.Read(buffer, 0, (int)length);

            return buffer.ReadCStrings();
        }

        public static string ReadCString(this Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            char[] charBuf = new char[1];
            byte[] readBuf = new byte[1];
            int len = 0;

            while (stream.Position < stream.Length && stream.Read(readBuf, 0, 1) == 1)
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
