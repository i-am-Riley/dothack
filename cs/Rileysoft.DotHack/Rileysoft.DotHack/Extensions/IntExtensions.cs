namespace Rileysoft.DotHack.Extensions
{
    public static class IntExtensions
    {
        public static string ToStringHexLE(this int value)
        {
            return BitConverter.GetBytes(value).ToStringHexLE();
        }
    }
}
