#pragma warning disable CA1707

namespace Rileysoft.DotHack.FileFormats.ELF
{
    public enum ELFMACHINE
    {
        /// <summary>
        /// An unknown machine
        /// </summary>
        EM_NONE,

        /// <summary>
        /// AT&T WE 32100
        /// </summary>
        EM_M32,

        /// <summary>
        /// Sun Microsystems SPARC
        /// </summary>
        EM_SPARC,

        /// <summary>
        /// Intel 80386
        /// </summary>
        EM_386,

        /// <summary>
        /// Motorola 68000
        /// </summary>
        EM_68K,

        /// <summary>
        /// Motorola 88000
        /// </summary>
        EM_88K,

        /// <summary>
        /// Intel 80860
        /// </summary>
        EM_860,

        /// <summary>
        /// MIPS RS3000 (big-endian only)
        /// </summary>
        EM_MIPS,

        /// <summary>
        /// HP/PA
        /// </summary>
        EM_PARISC,

        /// <summary>
        /// SPARC with enhanced instruction set
        /// </summary>
        EM_SPARC32PLUS,

        /// <summary>
        /// PowerPC
        /// </summary>
        EM_PPC,

        /// <summary>
        /// PowerPC 64-bit
        /// </summary>
        EM_PPC64,

        /// <summary>
        /// IBM S/390
        /// </summary>
        EM_S390,

        /// <summary>
        /// Advanced RISC Machines
        /// </summary>
        EM_ARM,

        /// <summary>
        /// Renesas SuperH
        /// </summary>
        EM_SH,

        /// <summary>
        /// SPARC v9 64-bit
        /// </summary>
        EM_SPARCV9,

        /// <summary>
        /// Intel Itanium
        /// </summary>
        EM_IA_64,

        /// <summary>
        /// AMD x86-64
        /// </summary>
        EM_X86_64,

        /// <summary>
        /// DEC Vax
        /// </summary>
        EM_VAX
    }
}

#pragma warning restore CA1707
