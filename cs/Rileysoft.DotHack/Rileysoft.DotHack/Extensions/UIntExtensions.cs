namespace Rileysoft.DotHack.Extensions
{
    public static class UIntExtensions
    {
        public static string ToStringHexLE(this uint value)
        {
            return BitConverter.GetBytes(value).ToStringHexLE();
        }
    }
}
