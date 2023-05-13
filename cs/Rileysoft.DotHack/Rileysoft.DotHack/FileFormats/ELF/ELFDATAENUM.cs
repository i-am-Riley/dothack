#pragma warning disable CA1712

namespace Rileysoft.DotHack.FileFormats.ELF
{
    public enum ELFDATA
    {
        /// <summary>
        /// Unknown data format.
        /// </summary>
        ELFDATANONE = 0,

        /// <summary>
        /// Two's complement, little-endian.
        /// </summary>
        ELFDATA2LSB = 1,

        /// <summary>
        /// Two's complement, big-endian.
        /// </summary>
        ELFDATA2MSB = 2
    }
}

#pragma warning restore CA1712
