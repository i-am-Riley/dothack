using Rileysoft.DotHack.Extensions;
using System.Diagnostics;

#pragma warning disable CA1707
#pragma warning disable IDE1006

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

            return e_ident.EI_DATA switch
            {
                ELFDATA.ELFDATA2LSB => stream.ReadUnsignedShortLE(),
                ELFDATA.ELFDATA2MSB => stream.ReadUnsignedShortBE(),
                _ => throw new InvalidOperationException(),
            };
        }

        private uint ReadUInt(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            if (e_ident == null)
                throw new InvalidOperationException();

            return e_ident.EI_DATA switch
            {
                ELFDATA.ELFDATA2LSB => stream.ReadUnsignedIntLE(),
                ELFDATA.ELFDATA2MSB => stream.ReadUnsignedIntBE(),
                _ => throw new InvalidOperationException(),
            };
        }

        private ulong ReadULong(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            if (e_ident == null)
                throw new InvalidOperationException();

            return e_ident.EI_DATA switch
            {
                ELFDATA.ELFDATA2LSB => stream.ReadUnsignedLongLE(),
                ELFDATA.ELFDATA2MSB => stream.ReadUnsignedLongBE(),
                _ => throw new InvalidOperationException(),
            };
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

    public class Elf32_Phdr
    {
        /// <summary>
        /// This member of the structure indicates what kind of
        /// segment this array element describes or how to interpret
        /// the array element's information.
        /// </summary>
        public ELFPTYPE p_type { get; set; } // uint

        /// <summary>
        /// This member holds the offset from the beginning of the
        /// file at which the first byte of the segment resides.
        /// </summary>
        public uint p_offset { get; set; } // uint

        /// <summary>
        /// This member holds the virtual address at which the first
        /// byte of the segment resides in memory.
        /// </summary>
        public uint p_vaddr { get; set; }

        /// <summary>
        /// On systems for which physical addressing is relevant, this
        /// member is reserved for the segment's physical address.
        /// Under BSD this member is not used and must be zero.
        /// </summary>
        public uint p_paddr { get; set; }

        /// <summary>
        /// This member holds the number of bytes in the file image of
        /// the segment.  It may be zero.
        /// </summary>
        public uint p_filesz { get; set; }

        /// <summary>
        /// This member holds the number of bytes in the memory image
        ///  of the segment.  It may be zero.
        /// </summary>
        public uint p_memsz { get; set; }

        /// <summary>
        /// This member holds a bit mask of flags relevant to the
        /// segment:
        /// 
        /// PF_X   An executable segment.
        /// PF_W   A writable segment.
        /// PF_R   A readable segment.
        ///
        /// A text segment commonly has the flags PF_X and PF_R.  A
        /// data segment commonly has PF_W and PF_R.
        /// </summary>
        public ELFPFLAGS p_flags { get; set; }

        /// <summary>
        /// This member holds the value to which the segments are
        /// aligned in memory and in the file.  Loadable process
        /// segments must have congruent values for p_vaddr and
        /// p_offset, modulo the page size.  Values of zero and one
        /// mean no alignment is required.  Otherwise, p_align should
        /// be a positive, integral power of two, and p_vaddr should
        /// equal p_offset, modulo p_align.
        /// </summary>
        public uint p_align { get; set; }

        public void ReadFromStream(Stream stream, bool bigEndian)
        {
            if (bigEndian)
            {
                p_type = (ELFPTYPE)stream.ReadUnsignedIntBE();
                p_offset = stream.ReadUnsignedIntBE();
                p_vaddr = stream.ReadUnsignedIntBE();
                p_paddr = stream.ReadUnsignedIntBE();
                p_filesz = stream.ReadUnsignedIntBE();
                p_memsz = stream.ReadUnsignedIntBE();
                p_flags = (ELFPFLAGS)stream.ReadUnsignedIntBE();
                p_align = stream.ReadUnsignedIntBE();
            }
            else
            {
                p_type = (ELFPTYPE)stream.ReadUnsignedIntLE();
                p_offset = stream.ReadUnsignedIntLE();
                p_vaddr = stream.ReadUnsignedIntLE();
                p_paddr = stream.ReadUnsignedIntLE();
                p_filesz = stream.ReadUnsignedIntLE();
                p_memsz = stream.ReadUnsignedIntLE();
                p_flags = (ELFPFLAGS)stream.ReadUnsignedIntLE();
                p_align = stream.ReadUnsignedIntLE();
            }
        }
    }

    [Flags]
    public enum ELFPFLAGS : uint
    {
        /// <summary>
        /// An executable segment.
        /// </summary>
        PF_X = 1,

        /// <summary>
        /// A writable segment.
        /// </summary>
        PF_W = 2,

        /// <summary>
        /// A readable segment.
        /// </summary>
        PF_R = 4
    }

    public enum ELFPTYPE : uint
    {
        /// <summary>
        /// The array element is unused and the other
        /// members' values are undefined.  This lets the
        /// program header have ignored entries.
        /// </summary>
        PT_NULL,

        /// <summary>
        /// The array element specifies a loadable segment,
        /// described by p_filesz and p_memsz.  The bytes
        /// from the file are mapped to the beginning of the
        /// memory segment.  If the segment's memory size
        /// p_memsz is larger than the file size p_filesz,
        /// the "extra" bytes are defined to hold the value
        /// 0 and to follow the segment's initialized area.
        /// The file size may not be larger than the memory
        /// size.  Loadable segment entries in the program
        /// header table appear in ascending order, sorted
        /// on the p_vaddr member.
        /// </summary>
        PT_LOAD,

        /// <summary>
        /// The array element specifies dynamic linking
        /// information.
        /// </summary>
        PT_DYNAMIC,

        /// <summary>
        /// The array element specifies the location and
        /// size of a null-terminated pathname to invoke as
        /// an interpreter.  This segment type is meaningful
        /// only for executable files (though it may occur
        /// for shared objects).  However it may not occur
        /// more than once in a file.  If it is present, it
        /// must precede any loadable segment entry.
        /// </summary>
        PT_INTERP,

        /// <summary>
        /// The array element specifies the location of
        /// notes (ElfN_Nhdr).
        /// </summary>
        PT_NOTE,

        /// <summary>
        /// This segment type is reserved but has
        /// unspecified semantics.  Programs that contain an
        /// array element of this type do not conform to the
        /// ABI.
        /// </summary>
        PT_SHLIB,

        /// <summary>
        /// The array element, if present, specifies the
        /// location and size of the program header table
        /// itself, both in the file and in the memory image
        /// of the program.  This segment type may not occur
        /// more than once in a file.  Moreover, it may
        /// occur only if the program header table is part
        /// of the memory image of the program.  If it is
        /// present, it must precede any loadable segment
        /// entry.
        /// </summary>
        PT_PHDR,

        /// <summary>
        /// Values in the inclusive range [PT_LOPROC,
        /// PT_HIPROC] are reserved for processor-specific
        /// semantics.
        /// </summary>
        PT_LOPROC,

        /// <summary>
        /// Values in the inclusive range [PT_LOPROC,
        /// PT_HIPROC] are reserved for processor-specific
        /// semantics.
        /// </summary>
        PT_HIPROC,

        /// <summary>
        /// GNU extension which is used by the Linux kernel
        /// to control the state of the stack via the flags
        /// set in the p_flags member.
        /// </summary>
        PT_GNU_STACK
    }

    public class Elf64_Phdr
    {
        /// <summary>
        /// This member of the structure indicates what kind of
        /// segment this array element describes or how to interpret
        /// the array element's information.
        /// </summary>
        public ELFPTYPE p_type { get; set; } // uint

        /// <summary>
        /// This member holds a bit mask of flags relevant to the
        /// segment:
        /// 
        /// PF_X   An executable segment.
        /// PF_W   A writable segment.
        /// PF_R   A readable segment.
        ///
        /// A text segment commonly has the flags PF_X and PF_R.  A
        /// data segment commonly has PF_W and PF_R.
        /// </summary>
        public ELFPFLAGS p_flags { get; set; } // uint

        /// <summary>
        /// This member holds the offset from the beginning of the
        /// file at which the first byte of the segment resides.
        /// </summary>
        public ulong p_offset { get; set; }

        /// <summary>
        /// This member holds the virtual address at which the first
        /// byte of the segment resides in memory.
        /// </summary>
        public ulong p_vaddr { get; set; }

        /// <summary>
        /// On systems for which physical addressing is relevant, this
        /// member is reserved for the segment's physical address.
        /// Under BSD this member is not used and must be zero.
        /// </summary>
        public ulong p_paddr { get; set; }

        /// <summary>
        /// This member holds the number of bytes in the file image of
        /// the segment.  It may be zero.
        /// </summary>
        public ulong p_filesz { get; set; }

        /// <summary>
        /// This member holds the number of bytes in the memory image
        ///  of the segment.  It may be zero.
        /// </summary>
        public ulong p_memsz { get; set; }

        /// <summary>
        /// This member holds the value to which the segments are
        /// aligned in memory and in the file.  Loadable process
        /// segments must have congruent values for p_vaddr and
        /// p_offset, modulo the page size.  Values of zero and one
        /// mean no alignment is required.  Otherwise, p_align should
        /// be a positive, integral power of two, and p_vaddr should
        /// equal p_offset, modulo p_align.
        /// </summary>
        public ulong p_align { get; set; }

        public void ReadFromStream(Stream stream, bool bigEndian)
        {
            if (bigEndian)
            {
                p_type = (ELFPTYPE)stream.ReadUnsignedIntBE();
                p_flags = (ELFPFLAGS)stream.ReadUnsignedIntBE();
                p_offset = stream.ReadUnsignedLongBE();
                p_vaddr = stream.ReadUnsignedLongBE();
                p_paddr = stream.ReadUnsignedLongBE();
                p_filesz = stream.ReadUnsignedLongBE();
                p_memsz = stream.ReadUnsignedLongBE();
                p_align = stream.ReadUnsignedLongBE();
            }
            else
            {
                p_type = (ELFPTYPE)stream.ReadUnsignedIntLE();
                p_flags = (ELFPFLAGS)stream.ReadUnsignedIntLE();
                p_offset = stream.ReadUnsignedLongLE();
                p_vaddr = stream.ReadUnsignedLongLE();
                p_paddr = stream.ReadUnsignedLongLE();
                p_filesz = stream.ReadUnsignedLongLE();
                p_memsz = stream.ReadUnsignedLongLE();
                p_align = stream.ReadUnsignedLongLE();
            }
        }
    }

    public enum SHDRTYPE
    {
        /// <summary>
        /// This value marks the section header as inactive.
        /// It does not have an associated section.  Other
        /// members of the section header have undefined
        /// values.
        /// </summary>
        SHT_NULL,

        /// <summary>
        /// This section holds information defined by the
        /// program, whose format and meaning are determined
        /// solely by the program.
        /// </summary>
        SHT_PROGBITS,

        /// <summary>
        /// This section holds a symbol table.  Typically,
        /// SHT_SYMTAB provides symbols for link editing,
        /// though it may also be used for dynamic linking.  As
        /// a complete symbol table, it may contain many
        /// symbols unnecessary for dynamic linking.  An object
        /// file can also contain a SHT_DYNSYM section.
        /// </summary>
        SHT_SYMTAB,

        /// <summary>
        /// This section holds a string table.  An object file
        /// may have multiple string table sections.
        /// </summary>
        SHT_STRTAB,

        /// <summary>
        /// This section holds relocation entries with explicit
        /// addends, such as type Elf32_Rela for the 32-bit
        /// class of object files.  An object may have multiple
        /// relocation sections.
        /// </summary>
        SHT_RELA,

        /// <summary>
        /// This section holds a symbol hash table.  An object
        /// participating in dynamic linking must contain a
        /// symbol hash table.  An object file may have only
        /// one hash table.
        /// </summary>
        SHT_HASH,

        /// <summary>
        /// This section holds information for dynamic linking.
        /// An object file may have only one dynamic section.
        /// </summary>
        SHT_DYNAMIC,

        /// <summary>
        /// This section holds notes (ElfN_Nhdr).
        /// </summary>
        SHT_NOTE,

        /// <summary>
        /// A section of this type occupies no space in the
        /// file but otherwise resembles SHT_PROGBITS.
        /// Although this section contains no bytes, the
        /// sh_offset member contains the conceptual file
        /// offset.
        /// </summary>
        SHT_NOBITS,

        /// <summary>
        /// This section holds relocation offsets without
        /// explicit addends, such as type Elf32_Rel for the
        /// 32-bit class of object files.  An object file may
        /// have multiple relocation sections.
        /// </summary>
        SHT_REL,

        /// <summary>
        /// This section is reserved but has unspecified
        /// semantics.
        /// </summary>
        SHT_SHLIB,
        
        /// <summary>
        /// This section holds a minimal set of dynamic linking
        /// symbols.  An object file can also contain a
        /// SHT_SYMTAB section.
        /// </summary>
        SHT_DYNSYM,

        /// <summary>
        /// Values in the inclusive range [SHT_LOPROC,
        /// SHT_HIPROC] are reserved for processor-specific
        /// semantics.
        /// </summary>
        SHT_LOPROC,

        /// <summary>
        /// Values in the inclusive range [SHT_LOPROC,
        /// SHT_HIPROC] are reserved for processor-specific
        /// semantics.
        /// </summary>
        SHT_HIPROC,

        /// <summary>
        /// This value specifies the lower bound of the range
        /// of indices reserved for application programs.
        /// </summary>
        SHT_LOUSER,

        /// <summary>
        /// This value specifies the upper bound of the range
        /// of indices reserved for application programs.
        /// Section types between SHT_LOUSER and SHT_HIUSER may
        /// be used by the application, without conflicting
        /// with current or future system-defined section
        /// types.
        /// </summary>
        SHT_HIUSER
    }

    public class Elf32_Shdr
    {
        public ElfData Parent { get; set; }

        /// <summary>
        /// This member specifies the name of the section.  Its value
        /// is an index into the section header string table section,
        /// giving the location of a null-terminated string.
        /// </summary>
        public uint sh_name { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// This member categorizes the section's contents and
        /// semantics.
        /// </summary>
        public uint sh_type { get; set; }

        /// <summary>
        /// Sections support one-bit flags that describe miscellaneous
        /// attributes.  If a flag bit is set in sh_flags, the
        /// attribute is "on" for the section.  Otherwise, the
        /// attribute is "off" or does not apply.  Undefined
        /// attributes are set to zero.
        /// </summary>
        public uint sh_flags { get; set; }

        /// <summary>
        /// If this section appears in the memory image of a process,
        /// this member holds the address at which the section's first
        /// byte should reside.  Otherwise, the member contains zero.
        /// </summary>
        public uint sh_addr { get; set; }

        /// <summary>
        /// This member's value holds the byte offset from the
        /// beginning of the file to the first byte in the section.
        /// One section type, SHT_NOBITS, occupies no space in the
        /// file, and its sh_offset member locates the conceptual
        /// placement in the file.
        /// </summary>
        public uint sh_offset { get; set; }

        /// <summary>
        /// This member holds the section's size in bytes.  Unless the
        /// section type is SHT_NOBITS, the section occupies sh_size
        /// bytes in the file.  A section of type SHT_NOBITS may have
        /// a nonzero size, but it occupies no space in the file.
        /// </summary>
        public uint sh_size { get; set; }

        /// <summary>
        /// This member holds a section header table index link, whose
        /// interpretation depends on the section type.
        /// </summary>
        public uint sh_link { get; set; }

        /// <summary>
        /// This member holds extra information, whose interpretation
        /// depends on the section type.
        /// </summary>
        public uint sh_info { get; set; }

        /// <summary>
        /// Some sections have address alignment constraints.  If a
        /// section holds a doubleword, the system must ensure
        /// doubleword alignment for the entire section.  That is, the
        /// value of sh_addr must be congruent to zero, modulo the
        /// value of sh_addralign.  Only zero and positive integral
        /// powers of two are allowed.  The value 0 or 1 means that
        /// the section has no alignment constraints.
        /// </summary>
        public uint sh_addralign { get; set; }

        /// <summary>
        /// Some sections hold a table of fixed-sized entries, such as
        /// a symbol table.  For such a section, this member gives the
        /// size in bytes for each entry.  This member contains zero
        /// if the section does not hold a table of fixed-size
        /// entries.
        /// </summary>
        public uint sh_entsize { get; set; }

        public void ReadFromStream (Stream stream, bool bigEndian)
        {
            if (bigEndian)
            {
                sh_name = stream.ReadUnsignedIntBE();
                sh_type = stream.ReadUnsignedIntBE();
                sh_flags = stream.ReadUnsignedIntBE();
                sh_addr = stream.ReadUnsignedIntBE();
                sh_offset = stream.ReadUnsignedIntBE();
                sh_size = stream.ReadUnsignedIntBE();
                sh_link = stream.ReadUnsignedIntBE();
                sh_info = stream.ReadUnsignedIntBE();
                sh_addralign = stream.ReadUnsignedIntBE();
                sh_entsize = stream.ReadUnsignedIntBE();
            }
            else
            {
                sh_name = stream.ReadUnsignedIntLE();
                sh_type = stream.ReadUnsignedIntLE();
                sh_flags = stream.ReadUnsignedIntLE();
                sh_addr = stream.ReadUnsignedIntLE();
                sh_offset = stream.ReadUnsignedIntLE();
                sh_size = stream.ReadUnsignedIntLE();
                sh_link = stream.ReadUnsignedIntLE();
                sh_info = stream.ReadUnsignedIntLE();
                sh_addralign = stream.ReadUnsignedIntLE();
                sh_entsize = stream.ReadUnsignedIntLE();
            }
        }
    }

    public class Elf64_Shdr
    {
        public ElfData Parent;

        /// <summary>
        /// This member specifies the name of the section.  Its value
        /// is an index into the section header string table section,
        /// giving the location of a null-terminated string.
        /// </summary>
        public uint sh_name { get; set; }

        public string Name { get; set; } = "";

        /// <summary>
        /// This member categorizes the section's contents and
        /// semantics.
        /// </summary>
        public uint sh_type { get; set; }

        /// <summary>
        /// Sections support one-bit flags that describe miscellaneous
        /// attributes.  If a flag bit is set in sh_flags, the
        /// attribute is "on" for the section.  Otherwise, the
        /// attribute is "off" or does not apply.  Undefined
        /// attributes are set to zero.
        /// </summary>
        public ulong sh_flags { get; set; }

        /// <summary>
        /// If this section appears in the memory image of a process,
        /// this member holds the address at which the section's first
        /// byte should reside.  Otherwise, the member contains zero.
        /// </summary>
        public ulong sh_addr { get; set; }

        /// <summary>
        /// This member's value holds the byte offset from the
        /// beginning of the file to the first byte in the section.
        /// One section type, SHT_NOBITS, occupies no space in the
        /// file, and its sh_offset member locates the conceptual
        /// placement in the file.
        /// </summary>
        public ulong sh_offset { get; set; }

        /// <summary>
        /// This member holds the section's size in bytes.  Unless the
        /// section type is SHT_NOBITS, the section occupies sh_size
        /// bytes in the file.  A section of type SHT_NOBITS may have
        /// a nonzero size, but it occupies no space in the file.
        /// </summary>
        public ulong sh_size { get; set; }

        /// <summary>
        /// This member holds a section header table index link, whose
        /// interpretation depends on the section type.
        /// </summary>
        public uint sh_link { get; set; }

        /// <summary>
        /// This member holds extra information, whose interpretation
        /// depends on the section type.
        /// </summary>
        public uint sh_info { get; set; }

        /// <summary>
        /// Some sections have address alignment constraints.  If a
        /// section holds a doubleword, the system must ensure
        /// doubleword alignment for the entire section.  That is, the
        /// value of sh_addr must be congruent to zero, modulo the
        /// value of sh_addralign.  Only zero and positive integral
        /// powers of two are allowed.  The value 0 or 1 means that
        /// the section has no alignment constraints.
        /// </summary>
        public ulong sh_addralign { get; set; }

        /// <summary>
        /// Some sections hold a table of fixed-sized entries, such as
        /// a symbol table.  For such a section, this member gives the
        /// size in bytes for each entry.  This member contains zero
        /// if the section does not hold a table of fixed-size
        /// entries.
        /// </summary>
        public ulong sh_entsize { get; set; }

        public void ReadFromStream(Stream stream, bool bigEndian)
        {
            if (bigEndian)
            {
                sh_name = stream.ReadUnsignedIntBE();
                sh_type = stream.ReadUnsignedIntBE();
                sh_flags = stream.ReadUnsignedLongBE();
                sh_addr = stream.ReadUnsignedLongBE();
                sh_offset = stream.ReadUnsignedLongBE();
                sh_size = stream.ReadUnsignedLongBE();
                sh_link = stream.ReadUnsignedIntBE();
                sh_info = stream.ReadUnsignedIntBE();
                sh_addralign = stream.ReadUnsignedLongBE();
                sh_entsize = stream.ReadUnsignedLongBE();
            }
            else
            {
                sh_name = stream.ReadUnsignedIntLE();
                sh_type = stream.ReadUnsignedIntLE();
                sh_flags = stream.ReadUnsignedLongLE();
                sh_addr = stream.ReadUnsignedLongLE();
                sh_offset = stream.ReadUnsignedLongLE();
                sh_size = stream.ReadUnsignedLongLE();
                sh_link = stream.ReadUnsignedIntLE();
                sh_info = stream.ReadUnsignedIntLE();
                sh_addralign = stream.ReadUnsignedLongLE();
                sh_entsize = stream.ReadUnsignedLongLE();
            }
        }
    }

    public class ElfData
    {
        public ElfHeader Ehdr { get; set; } = new ElfHeader();
        public List<Elf32_Phdr>? Phdr32s { get; set; }
        public List<Elf64_Phdr>? Phdr64s { get; set; }
        public List<Elf32_Shdr>? Shdr32s { get; set; }
        public List<Elf64_Shdr>? Shdr64s { get; set; }
        public byte[] Shstrs { get; set; }
        public List<string> Strtbl { get; set; }

        private ushort ReadUShort(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            if (Ehdr == null)
                throw new InvalidOperationException();

            if (Ehdr.e_ident == null)
                throw new InvalidOperationException();

            return Ehdr.e_ident.EI_DATA switch
            {
                ELFDATA.ELFDATA2LSB => stream.ReadUnsignedShortLE(),
                ELFDATA.ELFDATA2MSB => stream.ReadUnsignedShortBE(),
                _ => throw new InvalidOperationException(),
            };
        }

        private uint ReadUInt(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            if (Ehdr == null)
                throw new InvalidOperationException();

            if (Ehdr.e_ident == null)
                throw new InvalidOperationException();

            return Ehdr.e_ident.EI_DATA switch
            {
                ELFDATA.ELFDATA2LSB => stream.ReadUnsignedIntLE(),
                ELFDATA.ELFDATA2MSB => stream.ReadUnsignedIntBE(),
                _ => throw new InvalidOperationException(),
            };
        }

        private ulong ReadULong(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            if (Ehdr == null)
                throw new InvalidOperationException();

            if (Ehdr.e_ident == null)
                throw new InvalidOperationException();

            return Ehdr.e_ident.EI_DATA switch
            {
                ELFDATA.ELFDATA2LSB => stream.ReadUnsignedLongLE(),
                ELFDATA.ELFDATA2MSB => stream.ReadUnsignedLongBE(),
                _ => throw new InvalidOperationException(),
            };
        }

        public void ReadFromStream (Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            if (!stream.CanSeek)
                throw new ArgumentException("cannot seek");

            Ehdr.ReadFromStream(stream);

            if (Ehdr.e_ident == null)
                throw new InvalidOperationException();

            stream.Seek((long)Ehdr.e_phoff, SeekOrigin.Begin);

            switch (Ehdr.e_ident.EI_CLASS)
            {
                case ELFCLASS.ELFCLASS32:
                    Phdr32s = new List<Elf32_Phdr>();
                    for (int i=0; i< Ehdr.e_phnum; i++)
                    {
                        var Phdr32 = new Elf32_Phdr();
                        Phdr32.ReadFromStream(stream, Ehdr.e_ident.EI_DATA == ELFDATA.ELFDATA2MSB);
                        Phdr32s.Add(Phdr32);
                    }
                    
                    Shdr32s = new List<Elf32_Shdr>();
                    stream.Seek((long)Ehdr.e_shoff, SeekOrigin.Begin);
                    for (int i = 0; i < Ehdr.e_shnum; i++)
                    {
                        var Shdr32 = new Elf32_Shdr();
                        Shdr32.ReadFromStream(stream, Ehdr.e_ident.EI_DATA == ELFDATA.ELFDATA2MSB);
                        Shdr32.Parent = this;
                        Shdr32s.Add(Shdr32);


                        if (i == Ehdr.e_shstrndx)
                        {
                            long return_pos = stream.Position;
                            stream.Seek(Shdr32.sh_offset, SeekOrigin.Begin);
                            Shstrs = new byte[Shdr32.sh_size];
                            stream.Read(Shstrs, 0, (int)Shdr32.sh_size);
                            stream.Seek(return_pos, SeekOrigin.Begin);
                        }
                    }

                    foreach (var Shdr32 in Shdr32s)
                    {
                        Shdr32.Name = Shstrs.ReadCString(Shdr32.sh_name);
                    }

                    Strtbl = new List<string>();

                    foreach (var Shdr in Shdr32s)
                    {
                        if (Shdr.Name == ".strtab")
                        {
                            stream.Seek(Shdr.sh_offset, SeekOrigin.Begin);
                            while (stream.Position < stream.Length &&
                                stream.Position < Shdr.sh_offset+Shdr.sh_size)
                            {
                                Strtbl.Add(stream.ReadCString());
                            }
                        }
                    }

                    break;
                case ELFCLASS.ELFCLASS64:
                    Phdr64s = new List<Elf64_Phdr>();
                    for (int i=0; i < Ehdr.e_phnum; i++)
                    {
                        var Phdr64 = new Elf64_Phdr();
                        Phdr64.ReadFromStream(stream, Ehdr.e_ident.EI_DATA == ELFDATA.ELFDATA2MSB);
                        Phdr64s.Add(Phdr64);
                    }

                    Shdr64s = new List<Elf64_Shdr>();
                    stream.Seek((long)Ehdr.e_shoff, SeekOrigin.Begin);
                    for (int i=0; i<Ehdr.e_shnum; i++)
                    {
                        var Shdr64 = new Elf64_Shdr();
                        Shdr64.Parent = this;
                        Shdr64.ReadFromStream(stream, Ehdr.e_ident.EI_DATA == ELFDATA.ELFDATA2MSB);
                        Shdr64s.Add(Shdr64);

                        if (i == Ehdr.e_shstrndx)
                        {
                            long return_pos = stream.Position;
                            stream.Seek((long)Shdr64.sh_offset, SeekOrigin.Begin);
                            Shstrs = new byte[Shdr64.sh_size];
                            stream.Read(Shstrs, 0, (int)Shdr64.sh_size);
                            stream.Seek(return_pos, SeekOrigin.Begin);
                        }
                    }

                    foreach (var Shdr64 in Shdr64s)
                    {
                        Shdr64.Name = Shstrs.ReadCString(Shdr64.sh_name);
                    }

                    Strtbl = new List<string>();

                    foreach (var Shdr in Shdr64s)
                    {
                        if (Shdr.Name == ".strtab")
                        {
                            stream.Seek((long)Shdr.sh_offset, SeekOrigin.Begin);
                            while (stream.Position < stream.Length &&
                                stream.Position < (long)Shdr.sh_offset + (long)Shdr.sh_size)
                            {
                                Strtbl.Add(stream.ReadCString());
                            }
                        }
                    }

                    break;
                case ELFCLASS.ELFCLASSNONE:
                default:
                    throw new InvalidOperationException();
            }

            
        }
    }
}

#pragma warning restore IDE1006
#pragma warning restore CA1707
