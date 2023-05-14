#pragma warning disable CA1707
#pragma warning disable CA1712

namespace Rileysoft.DotHack.FileFormats.ELF
{
    public enum ELFOSABI
    {
        /// <summary>
        /// Same as ELFOSABI_SYSV
        /// </summary>
        ELFOSABI_NONE,

        /// <summary>
        /// UNIX System V ABI
        /// </summary>
        ELFOSABI_SYSV,

        /// <summary>
        /// HP-UX ABI
        /// </summary>
        ELFOSABI_HPUX,

        /// <summary>
        /// NetBSD ABI
        /// </summary>
        ELFOSABI_NETBSD,

        /// <summary>
        /// Linux ABI
        /// </summary>
        ELFOSABI_LINUX,

        /// <summary>
        /// Solaris ABI
        /// </summary>
        ELFOSABI_SOLARIS,

        /// <summary>
        /// IRIX ABI
        /// </summary>
        ELFOSABI_IRIX,

        /// <summary>
        /// FreeBSD ABI
        /// </summary>
        ELFOSABI_FREEBSD,

        /// <summary>
        /// TRU64 UNIX ABI
        /// </summary>
        ELFOSABI_TRU64,

        /// <summary>
        /// ARM architecture ABI
        /// </summary>
        ELFOSABI_ARM,

        /// <summary>
        /// Stand-alone (embedded) ABI
        /// </summary>
        ELFOSABI_STANDALONE
    }
}

#pragma warning restore CA1712
#pragma warning restore CA1707
