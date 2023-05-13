using Rileysoft.DotHack.Extensions;

#pragma warning disable CA1707

namespace Rileysoft.DotHack.FileFormats.ELF
{
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
}

#pragma warning restore CA1707
