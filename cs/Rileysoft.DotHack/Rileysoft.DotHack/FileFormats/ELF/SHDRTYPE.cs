#pragma warning disable CA1707

namespace Rileysoft.DotHack.FileFormats.ELF
{
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
}

#pragma warning restore CA1707
