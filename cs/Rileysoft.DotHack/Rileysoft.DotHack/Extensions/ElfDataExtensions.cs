using Rileysoft.FileFormats.ELF;
using Rileysoft.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Rileysoft.DotHack.Extensions
{
    public static class ElfDataExtensions
    {
        public static bool Is32Bit (this ElfData elfData)
        {
            if (elfData == null)
                throw new ArgumentNullException (nameof(elfData));

            if (elfData.Ehdr.e_ident.EI_CLASS == ELFCLASS.ELFCLASS32)
                return true;

            return false;
        }

        public static string GetComment (this ElfData elfData, Stream stream)
        {
            if (elfData == null)
                throw new ArgumentNullException(nameof(elfData));

            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            if (elfData.Is32Bit())
            {
                if (elfData.Shdr32s == null)
                    throw new ArgumentException($"{nameof(elfData.Shdr32s)} is null. can't read comment.");

                var commentSections = elfData.Shdr32s.Where(x => x.Name == ".comment");
                if (!commentSections.Any())
                    throw new ArgumentException("no .comment section. can't read comment.");

                var commentSection = commentSections.FirstOrDefault() ?? throw new ArgumentException("no .comment section. can't read comment.");
                stream.Seek(commentSection.sh_offset, SeekOrigin.Begin);

                return stream.ReadCString();
            }

            if (elfData.Shdr64s == null)
                throw new ArgumentException($"{nameof(elfData.Shdr64s)} is null. can't read comment.");

            var commentSections2 = elfData.Shdr64s.Where(x => x.Name == ".comment");
            if (!commentSections2.Any())
                throw new ArgumentException("no .comment section. can't read comment.");

            var commentSection2 = commentSections2.FirstOrDefault() ?? throw new ArgumentException("no .comment section. can't read comment.");
            stream.Seek((long)commentSection2.sh_offset, SeekOrigin.Begin);

            return stream.ReadCString();
        }

        public static void DbgIdent (this ElfData elfData)
        {
            if (elfData == null)
                throw new ArgumentNullException(nameof(elfData));

            Console.WriteLine("Ehdr e_ident");
            Console.WriteLine($" EI_MAG: {elfData.Ehdr.e_ident.EI_MAG0:X2} {elfData.Ehdr.e_ident.EI_MAG1:X2} {elfData.Ehdr.e_ident.EI_MAG2:X2} {elfData.Ehdr.e_ident.EI_MAG3:X2}");
            Console.WriteLine($"   -> (Header mismatch: {elfData.Ehdr.e_ident.HeaderMismatch})");
            Console.WriteLine($" EI_CLASS: {elfData.Ehdr.e_ident.EI_CLASS}");
            Console.WriteLine($" EI_DATA: {elfData.Ehdr.e_ident.EI_DATA}");
            Console.WriteLine($" EI_VERSION: {elfData.Ehdr.e_ident.EI_VERSION}");
            Console.WriteLine($" EI_OSABI: {elfData.Ehdr.e_ident.EI_OSABI}");
            Console.WriteLine("");
        }

        public static void DbgEhdr (this ElfData elfData)
        {
            if (elfData == null)
                throw new ArgumentNullException(nameof(elfData));

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

        }

        public static void DbgPhdr (this ElfData elfData)
        {
            if (elfData == null)
                throw new ArgumentNullException(nameof(elfData));

            if (elfData.Phdr32s != null)
            {
                Console.WriteLine("Phdr32s");
                for (int i = 0; i < elfData.Phdr32s.Count; i++)
                {
                    Console.WriteLine($"[{i}] Phdr32");
                    Console.WriteLine($" p_type: {elfData.Phdr32s[i].p_type}");
                    Console.WriteLine($" p_offset: {elfData.Phdr32s[i].p_offset:X8}");
                    Console.WriteLine($" p_vaddr: {elfData.Phdr32s[i].p_vaddr:X8}");
                    Console.WriteLine($" p_paddr: {elfData.Phdr32s[i].p_paddr:X8}");
                    Console.WriteLine($" p_filesz: {elfData.Phdr32s[i].p_filesz:X8}");
                    Console.WriteLine($" p_memsz: {elfData.Phdr32s[i].p_memsz:X8}");
                    Console.WriteLine($" p_flags: {elfData.Phdr32s[i].p_flags}");
                    Console.WriteLine($" p_align: {elfData.Phdr32s[i].p_align:X8}");
                }

            }

            if (elfData.Phdr64s != null)
            {
                Console.WriteLine("Phdr64s");
                for (int i = 0; i < elfData.Phdr64s.Count; i++)
                {
                    Console.WriteLine($"[{i}] Phdr64");
                    Console.WriteLine($" p_type: {elfData.Phdr64s[i].p_type}");
                    Console.WriteLine($" p_flags: {elfData.Phdr64s[i].p_flags}");
                    Console.WriteLine($" p_offset: {elfData.Phdr64s[i].p_offset:X16}");
                    Console.WriteLine($" p_vaddr: {elfData.Phdr64s[i].p_vaddr:X16}");
                    Console.WriteLine($" p_paddr: {elfData.Phdr64s[i].p_paddr:X16}");
                    Console.WriteLine($" p_filesz: {elfData.Phdr64s[i].p_filesz:X16}");
                    Console.WriteLine($" p_memsz: {elfData.Phdr64s[i].p_memsz:X16}");
                    Console.WriteLine($" p_align: {elfData.Phdr64s[i].p_align:X16}");
                }
            }

            Console.WriteLine("");
        }

        public static void DbgShdr (this ElfData elfData)
        {
            if (elfData == null)
                throw new ArgumentNullException(nameof(elfData));

            if (elfData.Shdr32s != null)
            {
                Console.WriteLine("Shdr32s");
                for (int i = 0; i < elfData.Shdr32s.Count; i++)
                {
                    string name;
                    var shdr32 = elfData.Shdr32s[i];
                    if (shdr32.Name == null)
                        name = "<null>";
                    else
                        name = shdr32.Name;

                    if (name.Length == 0)
                        name = "<null>";

                    Console.WriteLine($"[{i}] Shdr32 ({name})");
                    Console.WriteLine($" sh_name: {shdr32.sh_name:X8}");
                    Console.WriteLine($" sh_type: {shdr32.sh_type:X8}");
                    Console.WriteLine($" sh_flags: {shdr32.sh_flags:X8}");
                    Console.WriteLine($" sh_addr: {shdr32.sh_addr:X8}");
                    Console.WriteLine($" sh_offset: {shdr32.sh_offset:X8}");
                    Console.WriteLine($" sh_size: {shdr32.sh_size:X8}");
                    Console.WriteLine($" sh_link: {shdr32.sh_link:X8}");
                    Console.WriteLine($" sh_addralign: {shdr32.sh_addralign:X8}");
                    Console.WriteLine($" sh_entsize: {shdr32.sh_entsize:X8}");
                }
            }

            if (elfData.Shdr64s != null)
            {
                Console.WriteLine("Shdr64s");
                for (int i = 0; i < elfData.Shdr64s.Count; i++)
                {
                    string name;
                    var shdr64 = elfData.Shdr64s[i];
                    if (shdr64.Name == null)
                        name = "<null>";
                    else
                        name = shdr64.Name;

                    if (name.Length == 0)
                        name = "<null>";

                    Console.WriteLine($"[{i}] Shdr64 ({name})");
                    Console.WriteLine($" sh_name: {shdr64.sh_name:X8}");
                    Console.WriteLine($" sh_type: {shdr64.sh_type:X8}");
                    Console.WriteLine($" sh_flags: {shdr64.sh_flags:X16}");
                    Console.WriteLine($" sh_addr: {shdr64.sh_addr:X16}");
                    Console.WriteLine($" sh_offset: {shdr64.sh_offset:X16}");
                    Console.WriteLine($" sh_size: {shdr64.sh_size:X16}");
                    Console.WriteLine($" sh_link: {shdr64.sh_link:X8}");
                    Console.WriteLine($" sh_addralign: {shdr64.sh_addralign:X16}");
                    Console.WriteLine($" sh_entsize: {shdr64.sh_entsize:X16}");
                }
            }
        }

        public static void DumpStrtbl (this ElfData elfData, string path)
        {
            if (elfData == null)
                throw new ArgumentNullException(nameof(elfData));

            if (path == null)
                throw new ArgumentNullException(nameof(path));

            File.WriteAllLines(path, elfData.Strtbl);
        }
    }
}
