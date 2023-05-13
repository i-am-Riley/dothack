using Rileysoft.DotHack.Extensions;

namespace Rileysoft.DotHack.FileFormats.ELF
{
    // see https://man7.org/linux/man-pages/man5/elf.5.html
    // see https://upload.wikimedia.org/wikipedia/commons/e/e4/ELF_Executable_and_Linkable_Format_diagram_by_Ange_Albertini.png

    public class ElfData
    {
        public ElfHeader Ehdr { get; set; } = new ElfHeader();
        public List<Elf32_Phdr>? Phdr32s { get; set; }
        public List<Elf64_Phdr>? Phdr64s { get; set; }
        public List<Elf32_Shdr>? Shdr32s { get; set; }
        public List<Elf64_Shdr>? Shdr64s { get; set; }
        public byte[] Shstrs { get; set; }
        public List<string> Strtbl { get; set; }

        public ElfData()
        {
            Shstrs = Array.Empty<byte>();
            Strtbl = new List<string>();
        }

        public ElfData(Stream stream)
        {
            Shstrs = Array.Empty<byte>();
            Strtbl = new List<string>();
            ReadFromStream(stream);
        }

        public void ReadFromStream(Stream stream)
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
                    for (int i = 0; i < Ehdr.e_phnum; i++)
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
                                stream.Position < Shdr.sh_offset + Shdr.sh_size)
                            {
                                Strtbl.Add(stream.ReadCString());
                            }
                        }
                    }

                    break;
                case ELFCLASS.ELFCLASS64:
                    Phdr64s = new List<Elf64_Phdr>();
                    for (int i = 0; i < Ehdr.e_phnum; i++)
                    {
                        var Phdr64 = new Elf64_Phdr();
                        Phdr64.ReadFromStream(stream, Ehdr.e_ident.EI_DATA == ELFDATA.ELFDATA2MSB);
                        Phdr64s.Add(Phdr64);
                    }

                    Shdr64s = new List<Elf64_Shdr>();
                    stream.Seek((long)Ehdr.e_shoff, SeekOrigin.Begin);
                    for (int i = 0; i < Ehdr.e_shnum; i++)
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
