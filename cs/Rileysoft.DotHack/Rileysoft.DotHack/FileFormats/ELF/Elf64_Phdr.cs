using Rileysoft.DotHack.Extensions;

#pragma warning disable CA1707

namespace Rileysoft.DotHack.FileFormats.ELF
{
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
}

#pragma warning restore CA1707
