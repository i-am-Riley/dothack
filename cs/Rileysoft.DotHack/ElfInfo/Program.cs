// See https://aka.ms/new-console-template for more information
using Rileysoft.DotHack.FileFormats.ELF;

Console.WriteLine("ElfInfo");

while (true)
{
    try
    {
        Console.Write("Path: ");
        string? path = Console.ReadLine();
        if (path == null)
        {
            Console.WriteLine("no value");
            continue;
        }

        ElfData elfData = new ElfData();
        using (FileStream fs = File.OpenRead(path))
        {

            elfData.ReadFromStream(fs);
        }

        Console.WriteLine("Ehdr e_ident");
        Console.WriteLine($" EI_MAG: {elfData.Ehdr.e_ident.EI_MAG0:X2} {elfData.Ehdr.e_ident.EI_MAG1:X2} {elfData.Ehdr.e_ident.EI_MAG2:X2} {elfData.Ehdr.e_ident.EI_MAG3:X2}");
        Console.WriteLine($"   -> (Header mismatch: {elfData.Ehdr.e_ident.HeaderMismatch}");
        Console.WriteLine($" EI_CLASS: {elfData.Ehdr.e_ident.EI_CLASS}");
        Console.WriteLine($" EI_DATA: {elfData.Ehdr.e_ident.EI_DATA}");
        Console.WriteLine($" EI_VERSION: {elfData.Ehdr.e_ident.EI_VERSION}");
        Console.WriteLine($" EI_OSABI: {elfData.Ehdr.e_ident.EI_OSABI}");
        Console.WriteLine("");
        Console.WriteLine("Ehdr");
        Console.WriteLine($" e_type: {elfData.Ehdr.e_type}");
        Console.WriteLine($" e_machine: {elfData.Ehdr.e_machine}");
        Console.WriteLine($" e_version: {elfData.Ehdr.e_version}");
        Console.WriteLine($" e_entry: {elfData.Ehdr.e_entry:X16}");
        Console.WriteLine($" e_phoff: {elfData.Ehdr.e_phoff:X16}");
        Console.WriteLine($" e_shoff: {elfData.Ehdr.e_shoff:X16}");
        Console.WriteLine($" e_flags: {elfData.Ehdr.e_flags}");
        Console.WriteLine($" e_phentsize: {elfData.Ehdr.e_phentsize:X4}");
        Console.WriteLine($" e_phnum: {elfData.Ehdr.e_phnum:X4}");
        Console.WriteLine($" e_shentsize: {elfData.Ehdr.e_shentsize:X4}");
        Console.WriteLine($" e_shnum: {elfData.Ehdr.e_shnum:X4}");
        Console.WriteLine($" e_shstrndx: {elfData.Ehdr.e_shstrndx:X4}");
        Console.WriteLine("");
        
        if (elfData.Phdr32 != null)
        {
            Console.WriteLine("Phdr32");
            Console.WriteLine($" p_type: {elfData.Phdr32.p_type}");
            Console.WriteLine($" p_offset: {elfData.Phdr32.p_offset:X8}");
            Console.WriteLine($" p_vaddr: {elfData.Phdr32.p_vaddr:X8}");
            Console.WriteLine($" p_paddr: {elfData.Phdr32.p_paddr:X8}");
            Console.WriteLine($" p_filesz: {elfData.Phdr32.p_filesz:X8}");
            Console.WriteLine($" p_memsz: {elfData.Phdr32.p_memsz:X8}");
            Console.WriteLine($" p_flags: {elfData.Phdr32.p_flags}");
            Console.WriteLine($" p_align: {elfData.Phdr32.p_align:X8}");
        }

        if (elfData.Phdr64 != null)
        {
            Console.WriteLine("Phdr64");
            Console.WriteLine($" p_type: {elfData.Phdr64.p_type}");
            Console.WriteLine($" p_flags: {elfData.Phdr64.p_flags}");
            Console.WriteLine($" p_offset: {elfData.Phdr64.p_offset:X16}");
            Console.WriteLine($" p_vaddr: {elfData.Phdr64.p_vaddr:X16}");
            Console.WriteLine($" p_paddr: {elfData.Phdr64.p_paddr:X16}");
            Console.WriteLine($" p_filesz: {elfData.Phdr64.p_filesz:X16}");
            Console.WriteLine($" p_memsz: {elfData.Phdr64.p_memsz:X16}");
            Console.WriteLine($" p_align: {elfData.Phdr64.p_align:X16}");
        }

        // p_offset is the start of the MIPS assembly
        // p_offset + p_filesz will get you to section names
        // e_shoff will get you to the section headers
        // first section header is null
    }
    catch (Exception e)
    {
        Console.WriteLine(e.ToString());
    }
}


