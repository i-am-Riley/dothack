using Rileysoft.DotHack.Extensions;

#pragma warning disable CA1707

namespace Rileysoft.DotHack.FileFormats.ELF
{
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
}

#pragma warning restore CA1707
