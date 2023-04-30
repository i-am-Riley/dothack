namespace Rileysoft.DotHack.Extensions
{
    public static class ShortExtensions
    {
        public static string ToStringHexLE (this short value)
        {
            return BitConverter.GetBytes(value).ToStringHexLE();
        }
    }
}
