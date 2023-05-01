namespace Rileysoft.DotHack.Extensions
{
    public static class IntExtensions
    {
        public static string ToStringHexLE(this int value)
        {
            return BitConverter.GetBytes(value).ToStringHexLE();
        }

        public static string ToStringHexBE(this int value)
        {
            return BitConverter.GetBytes(value).ToStringHexBE();
        }
    }
}
