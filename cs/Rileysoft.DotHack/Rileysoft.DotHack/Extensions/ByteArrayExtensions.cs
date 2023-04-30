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

            uint a = 1;

            if (value > int.MaxValue)
            {
                return (int)((long)int.MinValue + (long)((long)value - (long)int.MaxValue - 1));
            }

            return (int)value;
        }
    }
}
