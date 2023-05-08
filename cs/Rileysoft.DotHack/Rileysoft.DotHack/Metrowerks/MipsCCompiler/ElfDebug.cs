using Rileysoft.DotHack.FileFormats.ELF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rileysoft.DotHack.Metrowerks.MipsCCompiler
{
    public class ElfDebug
    {
        public ElfData ElfData { get; set; }
        public bool Valid { get; set; }

        public byte[] Header { get; set; }
        public List<DebugSymbol> Symbols { get; set; }

        public ElfDebug(Stream stream)
        {
            ElfData = new ElfData();
            ElfData.ReadFromStream(stream);
        }

        public ElfDebug(ElfData elfData, Stream stream)
        {
            ElfData = elfData;
        }

        public void ReadFromStream (Stream stream)
        {
            if (ElfData.Shdr32s == null && ElfData.Shdr64s == null)
            {
                throw new InvalidOperationException("no sections");
            }

            long fileOffset = 0L;
            long sectionSize = 0L;
            switch (ElfData.Ehdr.e_ident.EI_CLASS)
            {
                case ELFCLASS.ELFCLASS32:
                    if (ElfData.Shdr32s == null)
                        throw new InvalidOperationException("no sections");

                    var debugSections = ElfData.Shdr32s.Where(x => x.Name == ".debug");
                    if (debugSections.Any())
                    {
                        var debugSection = debugSections.First();
                        fileOffset = (long)debugSection.sh_offset;
                        sectionSize = (long)debugSection.sh_size;
                    }
                    break;
                case ELFCLASS.ELFCLASS64:
                    if (ElfData.Shdr64s == null)
                        throw new InvalidOperationException("no sections");

                    var debugSections2 = ElfData.Shdr64s.Where(x => x.Name == ".debug");
                    if (debugSections2.Any())
                    {
                        var debugSection2 = debugSections2.First();
                        fileOffset = (long)debugSection2.sh_offset;
                        sectionSize = (long)debugSection2.sh_size;
                    }
                    break;
                case ELFCLASS.ELFCLASSNONE:
                default:
                    throw new NotImplementedException();
            }

            if (fileOffset == 0L || sectionSize == 0L)
            {
                Valid = false;
                return;
            }

            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            Header = new byte[0x1A];
            stream.Seek(fileOffset, SeekOrigin.Begin);
            stream.Read(Header, 0, Header.Length);

            Symbols = new List<DebugSymbol>();

            while (stream.Position < fileOffset+sectionSize)
            {
                Symbols.Add(new DebugSymbol(stream));
            }
        }
    }
}
