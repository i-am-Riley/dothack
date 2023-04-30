using System.Globalization;
using System.Text;

namespace Rileysoft.DotHack.Extensions
{
    public static class ByteArrayExtensions
    {
        public static ushort ReadUnsignedShort(this byte[] bytes, int offset = 0)
        {
            if (bytes == null) throw new ArgumentNullException(nameof(bytes));
            return (ushort)(
                (ushort)bytes[offset] +
                ((ushort)bytes[offset + 1] * (ushort)0x100));
        }

        public static uint ReadUnsignedInt(this byte[] bytes, int offset = 0)
        {
            if (bytes == null) throw new ArgumentNullException(nameof(bytes));
            return (uint)(
                (uint)bytes[offset] +
                ((uint)bytes[offset + 1] * (uint)0x100) +
                ((uint)bytes[offset + 2] * (uint)0x10000) +
                ((uint)bytes[offset + 3] * (uint)0x1000000));
        }

        public static int ReadInt(this byte[] bytes, int offset = 0)
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
                return (int)((long)int.MinValue + (long)((long)value - (long)int.MaxValue - 1));
            }

            return (int)value;
        }

        public static short ReadShort(this byte[] bytes, int offset = 0)
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

        public static IntPtr ReadIntPtr(this byte[] bytes, int offset = 0)
        {
            return new IntPtr(ReadInt(bytes, offset));
        }

        public static UIntPtr ReadUIntPtr(this byte[] bytes, int offset = 0)
        {
            return new UIntPtr(ReadUnsignedInt(bytes, offset));
        }

        public static string ToStringHexLE(this byte[] bytes, bool copy=false)
        {
            if (bytes == null)
                throw new ArgumentNullException(nameof(bytes));

            byte[] _bytes = bytes;

            if (!BitConverter.IsLittleEndian)
            {
                if (copy)
                {
                    _bytes = new byte[bytes.Length];
                    for (int i=0; i<bytes.Length; i++)
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

            for (int i=0; i<count; i++)
            {
                byte b = bytes[offset + i];

                if (i < count - 1)
                    sb.Append(b.ToString("X2", CultureInfo.InvariantCulture) + " ");
                else
                    sb.Append(b.ToString("X2", CultureInfo.InvariantCulture));
            }

            return sb.ToString();
        }
    }
}
