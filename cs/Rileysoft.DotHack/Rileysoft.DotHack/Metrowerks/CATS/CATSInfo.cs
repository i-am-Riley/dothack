using Rileysoft.DotHack.Extensions;
using Rileysoft.FileFormats.ELF;
using System.Collections.ObjectModel;

namespace Rileysoft.DotHack.Metrowerks.CATS
{
    /// <summary>
    /// .mwcats section
    /// </summary>
    public class CATSInfo
    {
        /// <summary>
        /// Reads all CATSInfos into a Collection
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static Collection<CATSInfo> ReadAllFromStream(Stream stream, ElfData elfData)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            if (elfData == null)
                throw new ArgumentNullException(nameof(elfData));

            Collection<CATSInfo> collection = new Collection<CATSInfo>();

            if (elfData.Is32Bit())
            {
                var sections32 = elfData.Shdr32s;
                if (sections32 == null)
                    throw new InvalidOperationException("32-bit Section Headers were not loaded.");

                foreach (var section in sections32)
                {
                    if (IsSection(section))
                    {
                        CATSInfo info32 = new(stream, section);
                        collection.Add(info32);
                    }
                }
            }
            else
            {
                var sections64 = elfData.Shdr64s;
                if (sections64 == null)
                    throw new InvalidOperationException("64-bit Section Headers were not loaded.");

                foreach (var section in sections64)
                {
                    if (IsSection(section))
                    {
                        CATSInfo info64 = new(stream, section);
                        collection.Add(info64);
                    }
                }
            }

            return collection;
        }

        /*
			*** CATS INFO (.mwcats) ***

00000000	Section type	2
00000001		nstd-exit	0
00000002		size		60
00000004		address		001000d0
        */

        public const string SectionName = ".mwcats";

        /// <summary>
        /// Checks if the provided section is a CATSInfo section
        /// </summary>
        public static bool IsSection(string sectionName)
        {
            return sectionName == SectionName;
        }

        /// <summary>
        /// Checks if the provided section is a CATSInfo section
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public static bool IsSection(Elf32_Shdr shdr)
        {
            if (shdr == null)
                throw new ArgumentNullException(nameof(shdr));

            return shdr.Name == SectionName;
        }

        /// <summary>
        /// Checks if the provided section is a CATSInfo section
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public static bool IsSection(Elf64_Shdr shdr)
        {
            if (shdr == null)
                throw new ArgumentNullException(nameof(shdr));

            return shdr.Name == SectionName;
        }

        /// <summary>
        /// Collection of CATSInfoSection
        /// </summary>
        public Collection<CATSInfoSection> Sections { get; }

        public CATSInfo()
        {
            Sections = new Collection<CATSInfoSection>();
        }

        /// <summary>
        /// Reads a .mwcats section from the stream.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public CATSInfo(Stream stream, Elf32_Shdr shdr) : this()
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            if (shdr == null)
                throw new ArgumentNullException(nameof(shdr));

            if (!IsSection(shdr))
                throw new ArgumentException("not a valid section for CATSInfo");

            ReadFromStream(stream, (long)shdr.sh_offset, (long)shdr.sh_size);
        }

        /// <summary>
        /// Reads a .mwcats section from the stream.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="shdr"></param>
        public CATSInfo(Stream stream, Elf64_Shdr shdr) : this()
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            if (shdr == null)
                throw new ArgumentNullException(nameof(shdr));

            if (!IsSection(shdr))
                throw new ArgumentException("not a valid section for CATSInfo");

            ReadFromStream(stream, (long)shdr.sh_offset, (long)shdr.sh_size);
        }

        /// <summary>
        /// Reads all of the CATSInfoSections from the stream.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void ReadFromStream(Stream stream, long size)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            if (!stream.CanRead)
                throw new ArgumentException("cannot read from stream");

            long endPos = stream.Position + size;

            Sections.Clear();

            while (stream.Position < endPos)
            {
                Sections.Add(new CATSInfoSection(stream));
            }
        }

        /// <summary>
        /// Seeks to the offset provided then reads 
        /// all of the CATSInfoSections from the stream.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void ReadFromStream(Stream stream, long offset, long size)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            if (!stream.CanRead)
                throw new ArgumentException("cannot read from stream");

            if (!stream.CanSeek)
                throw new ArgumentException("cannot seek in stream");

            stream.Seek(offset, SeekOrigin.Begin);
            ReadFromStream(stream, size);
        }
    }
}
