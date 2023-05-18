using Rileysoft.Common.Extensions;
using System.Collections.ObjectModel;

namespace Rileysoft.DotHack.Metrowerks.CATS
{
    /// <summary>
    /// Sections which appear in the .mwcats Shdrs in an ELF file
    /// </summary>
    public class CATSInfoSection
    {
        /* - Raw output from Metrowerks CATS
			*** CATS INFO (.mwcats) ***

00000000	Section type	2
00000001		nstd-exit	0
00000002		size		60
00000004		address		001000d0
...
00000068	Section type	2
00000069		nstd-exit	1
0000006a		size		20
0000006c		address		00100b80
        		offset
00000070		0000000c 
        */

        /// <summary>
        /// Formally called Section type by Metrowerks CATS.
        /// </summary>
        public byte SectionType { get; set; }

        /// <summary>
        /// Formally called nstd-exit by Metrowerks CATS.
        /// If set to <see langword="true"/>, the Offset property is read from the stream.
        /// </summary>
        public byte NSTDExit { get; set; }

        /// <summary>
        /// Formally called size by Metrowerks CATS.
        /// </summary>
        public ushort Size { get; set; }

        /// <summary>
        /// Formally called address by Metrowerks CATS.
        /// </summary>
        public int Address { get; set; }

        private int[] _Offset;

        /// <summary>
        /// Formally called offset by Metrowerks CATS.
        /// Returns a new <see cref="Collection{int}"/> created for an array of offsets
        /// with the same length as <see cref="NSTDExit"/>.
        /// </summary>
        public Collection<int> Offsets
        {
            get
            {
                return new Collection<int>(_Offset);
            }
        }

        public CATSInfoSection()
        {
            SectionType = 0;
            NSTDExit = 0;
            Size = 0;
            Address = 0;
            _Offset = Array.Empty<int>();
        }

        /// <summary>
        /// Reads the Section from the Stream
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public CATSInfoSection(Stream stream) : this()
        {
            ReadFromStream(stream);
        }

        /// <summary>
        /// Reads the Section from the Stream
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void ReadFromStream(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            if (!stream.CanRead)
                throw new ArgumentException("stream cannot be read");

            byte[] byteBuffer = new byte[1];

            stream.Read(byteBuffer, 0, 1);
            SectionType = byteBuffer[0];

            stream.Read(byteBuffer, 0, 1);
            NSTDExit = byteBuffer[0];

            Size = stream.ReadUnsignedShortLE();
            Address = stream.ReadIntLE();

            if (NSTDExit == 0)
                _Offset = Array.Empty<int>(); // Prevents allocation of an empty array.
            else
                _Offset = new int[NSTDExit];

            for (int i=0; i<NSTDExit; i++)
            {
                _Offset[i] = stream.ReadIntLE();
            }
        }
    }
}
