using Rileysoft.DotHack.Extensions;
using Rileysoft.DotHack.FileFormats.ELF;
using Rileysoft.DotHack.Metrowerks.MipsCCompiler.Statements;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rileysoft.DotHack.Metrowerks.MipsCCompiler
{
    public class ElfDebug
    {
        public ElfData ElfData { get; set; }
        public bool Valid { get; set; }
        public Collection<DebugFile> DebugFiles { get; set; }

        // The first bit of information at the top of the .debug section
        // is an integer. It's a relatively small integer but it isn't
        // representative of a number of items.
        // in the two files checked with this, the number of items is different
        // than this number.

        public int Num1 { get; set; }

        public ElfDebug(Stream stream)
        {
            ElfData = new ElfData();
            DebugFiles = new Collection<DebugFile>();
            ReadFromStream(stream);
        }

        public ElfDebug(ElfData elfData)
        {
            ElfData = elfData;
            DebugFiles = new Collection<DebugFile>();
        }

        public ElfDebug(ElfData elfData, Stream stream)
        {
            ElfData = elfData;
            DebugFiles = new Collection<DebugFile>();
            ReadFromStream(stream);
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

            stream.Seek(fileOffset, SeekOrigin.Begin);
            ElfDebugProcessor elfDebugProcessor = new ElfDebugProcessor();
            try
            {
                elfDebugProcessor.ReadFromStream(stream, sectionSize);
                DebugFiles = new Collection<DebugFile>();
                var debugFiles = elfDebugProcessor.DebugFiles;
                foreach (var debugFile in debugFiles)
                {
                    DebugFiles.Add(debugFile);
                }
            }
            catch
            {
                DebugFiles = new Collection<DebugFile>();
                var debugFiles = elfDebugProcessor.DebugFiles;
                foreach (var debugFile in debugFiles)
                {
                    DebugFiles.Add(debugFile);
                }
                throw;
            }
        }
    }
}
