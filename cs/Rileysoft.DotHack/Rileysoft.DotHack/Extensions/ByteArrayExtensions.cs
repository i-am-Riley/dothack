using System.Globalization;
using System.Text;

namespace Rileysoft.DotHack.Extensions
{
    public static class ByteArrayExtensions
    {
        public static ushort ReadUnsignedShortLE(this byte[] bytes, int offset = 0)
        {
            if (bytes == null) throw new ArgumentNullException(nameof(bytes));
            return (ushort)(
                (ushort)bytes[offset] +
                ((ushort)bytes[offset + 1] * (ushort)0x100));
        }

        public static uint ReadUnsignedIntLE(this byte[] bytes, int offset = 0)
        {
            if (bytes == null) throw new ArgumentNullException(nameof(bytes));
            return (uint)(
                (uint)bytes[offset] +
                ((uint)bytes[offset + 1] * (uint)0x100) +
                ((uint)bytes[offset + 2] * (uint)0x10000) +
                ((uint)bytes[offset + 3] * (uint)0x1000000));
        }

        public static int ReadIntLE(this byte[] bytes, int offset = 0)
        {
            if (bytes == null)
                throw new ArgumentNullException(nameof(bytes));

            uint value =
                (uint)bytes[offset] +
                (uint)bytes[offset + 1] * 0x100u +
                (uint)bytes[offset + 2] * 0x10000u +
                (uint)bytes[offset + 3] * 0x1000000u;

            if (value > int.MaxValue)
            {
                value -= int.MaxValue;
                int ivalue = (int)value;
                ivalue *= -1;
                ivalue = int.MinValue - ivalue - 1;

                return ivalue;
            }

            return (int)value;
        }

        public static ulong ReadUnsignedLongLE(this byte[] bytes, int offset = 0)
        {
            if (bytes == null)
                throw new ArgumentNullException(nameof(bytes));

            return (ulong)(
                (ulong)bytes[offset] +
                ((ulong)bytes[offset + 1] * (ulong)0x100UL) +
                ((ulong)bytes[offset + 2] * (ulong)0x10000UL) +
                ((ulong)bytes[offset + 3] * (ulong)0x1000000UL) +
                ((ulong)bytes[offset + 4] * (ulong)0x100000000UL) +
                ((ulong)bytes[offset + 5] * (ulong)0x10000000000UL) +
                ((ulong)bytes[offset + 6] * (ulong)0x1000000000000UL) +
                ((ulong)bytes[offset + 7] * (ulong)0x100000000000000UL));
        }

        public static short ReadShortLE(this byte[] bytes, int offset = 0)
        {
            if (bytes == null)
                throw new ArgumentNullException(nameof(bytes));

            ushort value = (ushort)(((ushort)bytes[offset]) + ((ushort)bytes[offset + 1]) * ((ushort)0x100));

            if (value > short.MaxValue)
            {
                return (short)((long)short.MinValue + (long)((long)value - (long)short.MaxValue - 1));
            }

            return (short)value;
        }

        public static IntPtr ReadIntPtrLE(this byte[] bytes, int offset = 0)
        {
            return new IntPtr(ReadIntLE(bytes, offset));
        }

        public static UIntPtr ReadUIntPtrLE(this byte[] bytes, int offset = 0)
        {
            return new UIntPtr(ReadUnsignedIntLE(bytes, offset));
        }

        public static ushort ReadUnsignedShortBE(this byte[] bytes, int offset = 0)
        {
            if (bytes == null) throw new ArgumentNullException(nameof(bytes));
            return (ushort)(
                (ushort)bytes[offset + 1] +
                ((ushort)bytes[offset] * (ushort)0x100));
        }

        public static uint ReadUnsignedIntBE(this byte[] bytes, int offset = 0)
        {
            if (bytes == null)
                throw new ArgumentNullException(nameof(bytes));

            return (uint)(
                (uint)bytes[offset + 3] +
                ((uint)bytes[offset + 2] * (uint)0x100) +
                ((uint)bytes[offset + 1] * (uint)0x10000) +
                ((uint)bytes[offset] * (uint)0x1000000));
        }

        public static ulong ReadUnsignedLongBE(this byte[] bytes, int offset = 0)
        {
            if (bytes == null)
                throw new ArgumentNullException(nameof(bytes));

            return (ulong)(
                (ulong)bytes[offset + 7] +
                ((ulong)bytes[offset + 6] * (ulong)0x100) +
                ((ulong)bytes[offset + 5] * (ulong)0x10000) +
                ((ulong)bytes[offset + 4] * (ulong)0x1000000) +
                ((ulong)bytes[offset + 3] * (ulong)0x100000000) +
                ((ulong)bytes[offset + 2] * (ulong)0x10000000000) +
                ((ulong)bytes[offset + 1] * (ulong)0x1000000000000) +
                ((ulong)bytes[offset] * (ulong)0x100000000000000));
        }

        public static int ReadIntBE(this byte[] bytes, int offset = 0)
        {
            if (bytes == null)
                throw new ArgumentNullException(nameof(bytes));

            uint value =
                (uint)bytes[offset + 3] +
                (uint)bytes[offset + 2] * 0x100u +
                (uint)bytes[offset + 1] * 0x10000u +
                (uint)bytes[offset] * 0x1000000u;

            if (value > int.MaxValue)
            {
                return (int)((long)int.MinValue + (long)((long)value - (long)int.MaxValue - 1));
            }

            return (int)value;
        }

        public static long ReadLongBE(this byte[] bytes, int offset = 0)
        {
            if (bytes == null)
                throw new ArgumentNullException(nameof(bytes));

            ulong value = ReadUnsignedLongBE(bytes, offset);

            if (value > long.MaxValue)
            {
                value -= long.MaxValue;
                long lvalue = (long)value;
                lvalue *= -1;
                lvalue = long.MinValue - lvalue - 1;

                return lvalue;
            }

            return (long)value;
        }

        public static long ReadLongLE(this byte[] bytes, int offset = 0)
        {
            if (bytes == null)
                throw new ArgumentNullException(nameof(bytes));

            ulong value = ReadUnsignedLongLE(bytes, offset);

            if (value > long.MaxValue)
            {
                value -= long.MaxValue;
                long lvalue = (long)value;
                lvalue *= -1;
                lvalue = long.MinValue - lvalue - 1;

                return lvalue;
            }

            return (long)value;
        }

        public static short ReadShortBE(this byte[] bytes, int offset = 0)
        {
            if (bytes == null)
                throw new ArgumentNullException(nameof(bytes));

            ushort value = (ushort)(((ushort)bytes[offset + 1]) +
                ((ushort)bytes[offset]) * ((ushort)0x100));

            if (value > short.MaxValue)
            {
                return (short)((long)short.MinValue + (long)((long)value - (long)short.MaxValue - 1));
            }

            return (short)value;
        }

        public static IntPtr ReadIntPtrBE(this byte[] bytes, int offset = 0)
        {
            return new IntPtr(ReadIntBE(bytes, offset));
        }

        public static UIntPtr ReadUIntPtrBE(this byte[] bytes, int offset = 0)
        {
            return new UIntPtr(ReadUnsignedIntBE(bytes, offset));
        }

        public static string ToStringHexLE(this byte[] bytes, bool copy = false)
        {
            if (bytes == null)
                throw new ArgumentNullException(nameof(bytes));

            byte[] _bytes = bytes;

            if (!BitConverter.IsLittleEndian)
            {
                if (copy)
                {
                    _bytes = new byte[bytes.Length];
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        _bytes[i] = bytes[i];
                    }
                }

                Array.Reverse(_bytes);
            }

            return string.Join("", bytes.Select(b => b.ToString("X2", CultureInfo.InvariantCulture)));
        }

        public static string ToStringHexBE(this byte[] bytes, bool copy = false)
        {
            if (bytes == null)
                throw new ArgumentNullException(nameof(bytes));

            byte[] _bytes = bytes;

            if (BitConverter.IsLittleEndian)
            {
                if (copy)
                {
                    _bytes = new byte[bytes.Length];
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        _bytes[i] = bytes[i];
                    }
                }

                Array.Reverse(_bytes);
            }

            return string.Join("", bytes.Select(b => b.ToString("X2", CultureInfo.InvariantCulture)));
        }

        public static string ToStringHexExpanded(this byte[] bytes, int offset = 0, int count = -1)
        {
            if (bytes == null)
                throw new ArgumentNullException(nameof(bytes));

            if (count == -1)
                count = bytes.Length - offset;

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < count; i++)
            {
                byte b = bytes[offset + i];

                if (i < count - 1)
                    sb.Append(b.ToString("X2", CultureInfo.InvariantCulture) + " ");
                else
                    sb.Append(b.ToString("X2", CultureInfo.InvariantCulture));
            }

            return sb.ToString();
        }

        public static List<string> ReadCStrings(this byte[] bytes)
        {
            List<string> strings = new List<string>();
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                while (ms.Position < ms.Length)
                {
                    strings.Add(ms.ReadCString());
                }
            }

            return strings;
        }

        public static string ReadCString(this byte[] bytes, long offset = 0)
        {
            if (bytes == null)
                throw new ArgumentNullException(nameof(bytes));

            char[] charBuf = new char[1];
            int len = 0;

            while (offset + len < bytes.Length)
            {
                byte b = bytes[offset + len];

                if (b == 0)
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

                charBuf[len++] = Convert.ToChar(b);
            }

            return new string(charBuf, 0, len);
        }
    }
}
