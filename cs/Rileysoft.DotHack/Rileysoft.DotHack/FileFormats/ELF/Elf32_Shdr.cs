using Rileysoft.DotHack.Extensions;

#pragma warning disable CA1707

namespace Rileysoft.DotHack.FileFormats.ELF
{
    public class Elf32_Shdr
    {
        public ElfData? Parent { get; set; }

        /// <summary>
        /// This member specifies the name of the section.  Its value
        /// is an index into the section header string table section,
        /// giving the location of a null-terminated string.
        /// </summary>
        public uint sh_name { get; set; }

        public string? Name { get; set; }

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

        public void ReadFromStream(Stream stream, bool bigEndian)
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
}

#pragma warning restore CA1707
