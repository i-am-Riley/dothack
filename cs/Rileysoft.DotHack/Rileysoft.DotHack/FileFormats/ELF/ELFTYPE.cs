#pragma warning disable CA1707

namespace Rileysoft.DotHack.FileFormats.ELF
{
    public enum ELFTYPE
    {
        /// <summary>
        /// An unknown type.
        /// </summary>
        ET_NONE,

        /// <summary>
        /// A relocatable file.
        /// </summary>
        ET_REL,

        /// <summary>
        /// An executable file.
        /// </summary>
        ET_EXEC,

        /// <summary>
        /// A shared object.
        /// </summary>
        ET_DYN,

        /// <summary>
        /// A core file.
        /// </summary>
        ET_CORE
    }
}

#pragma warning restore CA1707
