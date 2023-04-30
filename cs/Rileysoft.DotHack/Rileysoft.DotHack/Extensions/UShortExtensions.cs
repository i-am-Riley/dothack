namespace Rileysoft.DotHack.Extensions
{
    public static class UShortExtensions
    {
        public static string ToStringHexLE(this ushort value)
        {
            return BitConverter.GetBytes(value).ToStringHexLE();
        }
    }
}
