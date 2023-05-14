#pragma warning disable CA1028
#pragma warning disable CA1707

namespace Rileysoft.DotHack.FileFormats.ELF
{
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
}

#pragma warning restore CA1707
#pragma warning restore CA1028
