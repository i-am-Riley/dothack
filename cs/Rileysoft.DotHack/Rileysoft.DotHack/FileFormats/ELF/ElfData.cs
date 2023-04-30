using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rileysoft.DotHack.FileFormats.ELF
{
    // see https://man7.org/linux/man-pages/man5/elf.5.html
    // see https://upload.wikimedia.org/wikipedia/commons/e/e4/ELF_Executable_and_Linkable_Format_diagram_by_Ange_Albertini.png
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

    public enum ELFVERSION
    {
        EV_NONE = 0,
        EV_CURRENT = 1
    }

    public enum ELFOSABI
    {
        ELFOSABI_NONE,
        ELFOSABI_SYSV,
        ELFOSABI_HPUX,
        ELFOSABI_NETBSD,
        ELFOSABI_LINUX,
        ELFOSABI_SOLARIS,
        ELFOSABI_IRIX,
        ELFOSABI_FREEBSD,
        ELFOSABI_TRU64,
        ELFOSABI_ARM,
        ELFOSABI_STANDALONE
    }

    public class ElfData
    {
        public static readonly byte[] EI_MAG = new byte[] { 0x74, 0x45, 0x4C, 0x46 };
        public ELFCLASS EI_CLASS { get; set; } = ELFCLASS.ELFCLASSNONE;
        public ELFDATA EI_DATA { get; set; } = ELFDATA.ELFDATANONE;
        public ELFVERSION EI_VERSION { get; set; } = ELFVERSION.EV_NONE;
        public ELFOSABI EI_OSABI { get; set; } = ELFOSABI.ELFOSABI_NONE;
        public byte EI_ABIVERSION { get; set; }



    }
}
