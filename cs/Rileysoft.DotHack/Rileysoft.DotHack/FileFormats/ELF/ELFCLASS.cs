#pragma warning disable CA1712 // disable ALLCAPS warning

namespace Rileysoft.DotHack.FileFormats.ELF
{
    public enum ELFCLASS
    {
        /// <summary>
        /// This class is invalid.
        /// </summary>
        ELFCLASSNONE = 0,

        /// <summary>
        /// This defines the 32-bit architecture.  
        /// It supports machines with files and virtual
        /// address spaces up to 4 Gigabytes.
        /// </summary>
        ELFCLASS32 = 1,

        /// <summary>
        /// This defines the 64-bit architecture.
        /// </summary>
        ELFCLASS64 = 2
    }
}

#pragma warning restore CA1712
