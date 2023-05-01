using Rileysoft.DotHack.Extensions;
using Rileysoft.DotHack.SLUS20267;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

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
        /// <summary>
        /// Invalid version.
        /// </summary>
        EV_NONE = 0,

        /// <summary>
        /// Current version.
        /// </summary>
        EV_CURRENT = 1
    }

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

    public class ElfIdentity
    {
        public const byte ELFMAG0 = 0x7F;
        public const byte ELFMAG1 = 0x45;
        public const byte ELFMAG2 = 0x4C;
        public const byte ELFMAG3 = 0x46;

        /// <summary>
        /// The first byte of the magic number.  It must be
        /// filled with ELFMAG0.  (0: 0x7f)
        /// </summary>
        public byte EI_MAG0 { get; set; }

        /// <summary>
        /// The second byte of the magic number.  It must be
        /// filled with ELFMAG1.  (1: 'E')
        /// </summary>
        public byte EI_MAG1 { get; set; }

        /// <summary>
        /// The third byte of the magic number.  It must be
        /// filled with ELFMAG2.  (2: 'L')
        /// </summary>
        public byte EI_MAG2 { get; set; }

        /// <summary>
        /// The fourth byte of the magic number.  It must be
        /// filled with ELFMAG3.  (3: 'F')
        /// </summary>
        public byte EI_MAG3 { get; set; }

        /// <summary>
        /// The fifth byte identifies the architecture for this binary
        /// </summary>
        public ELFCLASS EI_CLASS { get; set; } = ELFCLASS.ELFCLASSNONE;

        /// <summary>
        /// The sixth byte specifies the data encoding of the
        /// processor-specific data in the file.
        /// </summary>
        public ELFDATA EI_DATA { get; set; } = ELFDATA.ELFDATANONE;

        /// <summary>
        /// The seventh byte is the version number of the ELF
        /// specification
        /// </summary>
        public ELFVERSION EI_VERSION { get; set; } = ELFVERSION.EV_NONE;

        /// <summary>
        /// The eighth byte identifies the operating system and
        /// ABI to which the object is targeted.  Some fields
        /// in other ELF structures have flags and values that
        /// have platform-specific meanings; the interpretation
        /// of those fields is determined by the value of this
        /// byte.
        /// </summary>
        public ELFOSABI EI_OSABI { get; set; } = ELFOSABI.ELFOSABI_NONE;

        /// <summary>
        /// The ninth byte identifies the version of the ABI to
        /// which the object is targeted.  This field is used
        /// to distinguish among incompatible versions of an
        /// ABI.  The interpretation of this version number is
        /// dependent on the ABI identified by the EI_OSABI
        /// field.  Applications conforming to this
        /// specification use the value 0.
        /// </summary>
        public byte EI_ABIVERSION { get; set; }
        public bool HeaderMismatch { get; private set; }

        public void ReadFromStream (Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            byte[] data = new byte[16];
            stream.Read(data, 0, 16);

            EI_MAG0 = data[0];
            EI_MAG1 = data[1];
            EI_MAG2 = data[2];
            EI_MAG3 = data[3];

            bool HeaderMismatch =
                (EI_MAG0 != ELFMAG0) ||
                (EI_MAG1 != ELFMAG1) ||
                (EI_MAG2 != ELFMAG2) ||
                (EI_MAG3 != ELFMAG3);

            if (HeaderMismatch)
                Debug.WriteLine($"Header mismatched: \nGot: {data.ToStringHexExpanded(0, 4)}");

            EI_CLASS = (ELFCLASS)data[4];
            EI_DATA = (ELFDATA)data[5];
            EI_VERSION = (ELFVERSION)data[6];
            EI_OSABI = (ELFOSABI)data[7];
            EI_ABIVERSION = data[8];
        }
    }

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

    public class ElfHeader
    {
        /// <summary>
        /// This array of bytes specifies how to interpret the file,
        /// independent of the processor or the file's remaining
        /// contents.  Within this array everything is named by
        /// macros, which start with the prefix EI_ and may contain
        /// values which start with the prefix ELF.
        /// </summary>
        public ElfIdentity e_ident { get; set; } = new ElfIdentity();

        /// <summary>
        /// This member of the structure identifies the object file type
        /// </summary>
        public ELFTYPE e_type { get; set; } = ELFTYPE.ET_NONE;

        /// <summary>
        /// This member specifies the required architecture for an individual file.
        /// </summary>
        public ELFMACHINE e_machine { get; set; }

        /// <summary>
        /// This member identifies the file version
        /// </summary>
        public ELFVERSION e_version { get; set; }

        /// <summary>
        /// This member gives the virtual address to which the system
        /// first transfers control, thus starting the process. If
        /// the file has no associated entry point, this member holds
        /// zero.
        /// </summary>
        public ulong e_entry { get; set; } // dependent on 32/64

        /// <summary>
        /// This member holds the program header table's file offset
        /// in bytes.If the file has no program header table, this
        /// member holds zero.
        /// </summary>
        public ulong e_phoff { get; set; } // dependent on 32/64

        /// <summary>
        /// This member holds the section header table's file offset
        /// in bytes.If the file has no section header table, this
        /// member holds zero.
        /// </summary>
        public ulong e_shoff { get; set; } // dependent on 32/64

        /// <summary>
        /// This member holds processor-specific flags associated with
        /// the file.Flag names take the form EF_`machine_flag'.
        /// Currently, no flags have been defined.
        /// </summary>
        public uint e_flags { get; set; }

        /// <summary>
        /// This member holds the ELF header's size in bytes.
        /// </summary>
        public ushort e_ehsize { get; set; }

        /// <summary>
        /// This member holds the size in bytes of one entry in the
        /// file's program header table; all entries are the same
        /// size.
        /// </summary>
        public ushort e_phentsize { get; set; }

        /// <summary>
        /// This member holds the number of entries in the program
        /// header table.  Thus the product of e_phentsize and e_phnum
        /// gives the table's size in bytes.  If a file has no program
        /// header, e_phnum holds the value zero.
        /// 
        /// If the number of entries in the program header table is
        /// larger than or equal to PN_XNUM (0xffff), this member
        /// holds PN_XNUM (0xffff) and the real number of entries in
        /// the program header table is held in the sh_info member of
        /// the initial entry in section header table.  Otherwise, the
        /// sh_info member of the initial entry contains the value
        /// zero.
        /// </summary>
        public ushort e_phnum { get; set; }

        /// <summary>
        /// This member holds a sections header's size in bytes.  A
        /// section header is one entry in the section header table;
        /// all entries are the same size.
        /// </summary>
        public ushort e_shentsize { get; set; }

        /// <summary>
        /// This member holds the number of entries in the section
        /// header table.  Thus the product of e_shentsize and e_shnum
        /// gives the section header table's size in bytes.  If a file
        /// has no section header table, e_shnum holds the value of
        /// zero.
        /// 
        /// If the number of entries in the section header table is
        /// larger than or equal to SHN_LORESERVE (0xff00), e_shnum
        /// holds the value zero and the real number of entries in the
        /// section header table is held in the sh_size member of the
        /// initial entry in section header table.  Otherwise, the
        /// sh_size member of the initial entry in the section header
        /// table holds the value zero.
        /// </summary>
        public ushort e_shnum { get; set; }

        /// <summary>
        /// This member holds the section header table index of the
        /// entry associated with the section name string table.  If
        /// the file has no section name string table, this member
        /// holds the value SHN_UNDEF.
        ///
        /// If the index of section name string table section is
        /// larger than or equal to SHN_LORESERVE (0xff00), this
        /// member holds SHN_XINDEX (0xffff) and the real index of the
        /// section name string table section is held in the sh_link
        /// member of the initial entry in section header table.
        /// Otherwise, the sh_link member of the initial entry in
        /// section header table contains the value zero.
        /// </summary>
        public ushort e_shstrndx { get; set; }

        /// <summary>
        /// This is defined as 0xffff, the largest number
        /// e_phnum can have, specifying where the actual
        /// number of program headers is assigned.
        /// </summary>
        public const ushort PN_XNUM = 0xFFFF;
        public const ushort SHN_UNDEF = 0x0000;
        public const ushort SHN_LORESERVE = 0xFF00;
        public const ushort SHN_XINDEX = 0xFFFF;

        private ushort ReadUShort(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            if (e_ident == null)
                throw new InvalidOperationException();

            switch (e_ident.EI_DATA)
            {
                case ELFDATA.ELFDATA2LSB:
                    return stream.ReadUnsignedShortLE();
                case ELFDATA.ELFDATA2MSB:
                    return stream.ReadUnsignedShortBE();
                case ELFDATA.ELFDATANONE:
                default:
                    throw new InvalidOperationException();
            }
        }

        private uint ReadUInt(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            if (e_ident == null)
                throw new InvalidOperationException();

            switch (e_ident.EI_DATA)
            {
                case ELFDATA.ELFDATA2LSB:
                    return stream.ReadUnsignedIntLE();
                case ELFDATA.ELFDATA2MSB:
                    return stream.ReadUnsignedIntBE();
                case ELFDATA.ELFDATANONE:
                default:
                    throw new InvalidOperationException();
            }
        }

        private ulong ReadULong(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            if (e_ident == null)
                throw new InvalidOperationException();

            switch (e_ident.EI_DATA)
            {
                case ELFDATA.ELFDATA2LSB:
                    return stream.ReadUnsignedLongLE();
                case ELFDATA.ELFDATA2MSB:
                    return stream.ReadUnsignedLongBE();
                case ELFDATA.ELFDATANONE:
                default:
                    throw new InvalidOperationException();
            }
        }

        public void ReadFromStream(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            e_ident.ReadFromStream(stream);
            e_type = (ELFTYPE)ReadUShort(stream);
            e_machine = (ELFMACHINE)ReadUShort(stream);
            e_version = (ELFVERSION)ReadUInt(stream);

            switch (e_ident.EI_CLASS)
            {
                case ELFCLASS.ELFCLASS32:
                    e_entry = (ulong)ReadUInt(stream);
                    e_phoff = (ulong)ReadUInt(stream);
                    e_shoff = (ulong)ReadUInt(stream);
                    break;
                case ELFCLASS.ELFCLASS64:
                    e_entry = ReadULong(stream);
                    e_phoff = ReadULong(stream);
                    e_shoff = ReadULong(stream);
                    break;
                case ELFCLASS.ELFCLASSNONE:
                default:
                    throw new InvalidOperationException("invalid elf class");
            }

            e_flags = ReadUInt(stream);
            e_ehsize = ReadUShort(stream);
            e_phentsize = ReadUShort(stream);
            e_phnum = ReadUShort(stream);
            e_shentsize = ReadUShort(stream);
            e_shnum = ReadUShort(stream);
            e_shstrndx = ReadUShort(stream);
        }
    }

    public class ElfData
    {
        public ElfHeader Ehdr { get; set; } = new ElfHeader();

        private ushort ReadUShort(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            if (Ehdr == null)
                throw new InvalidOperationException();

            if (Ehdr.e_ident == null)
                throw new InvalidOperationException();

            switch (Ehdr.e_ident.EI_DATA)
            {
                case ELFDATA.ELFDATA2LSB:
                    return stream.ReadUnsignedShortLE();
                case ELFDATA.ELFDATA2MSB:
                    return stream.ReadUnsignedShortBE();
                case ELFDATA.ELFDATANONE:
                default:
                    throw new InvalidOperationException();
            }
        }

        private uint ReadUInt(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            if (Ehdr == null)
                throw new InvalidOperationException();

            if (Ehdr.e_ident == null)
                throw new InvalidOperationException();

            switch (Ehdr.e_ident.EI_DATA)
            {
                case ELFDATA.ELFDATA2LSB:
                    return stream.ReadUnsignedIntLE();
                case ELFDATA.ELFDATA2MSB:
                    return stream.ReadUnsignedIntBE();
                case ELFDATA.ELFDATANONE:
                default:
                    throw new InvalidOperationException();
            }
        }

        private ulong ReadULong(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            if (Ehdr == null)
                throw new InvalidOperationException();

            if (Ehdr.e_ident == null)
                throw new InvalidOperationException();

            switch (Ehdr.e_ident.EI_DATA)
            {
                case ELFDATA.ELFDATA2LSB:
                    return stream.ReadUnsignedLongLE();
                case ELFDATA.ELFDATA2MSB:
                    return stream.ReadUnsignedLongBE();
                case ELFDATA.ELFDATANONE:
                default:
                    throw new InvalidOperationException();
            }
        }

        public void ReadFromStream (Stream stream)
        {
            Ehdr.ReadFromStream(stream);
        }
    }
}
