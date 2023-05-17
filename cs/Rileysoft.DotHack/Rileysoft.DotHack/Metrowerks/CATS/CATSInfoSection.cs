using Rileysoft.Common.Extensions;

namespace Rileysoft.DotHack.Metrowerks.CATS
{
    public class CATSInfoSection
    {
        /*
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

        public byte SectionType { get; set; }
        public bool NSTDExit { get; set; }
        public ushort Size { get; set; }
        public int Address { get; set; }

        /// <summary>
        /// Only read if NSTDExit is true.
        /// </summary>
        public int Offset { get; set; }

        public CATSInfoSection()
        {
            SectionType = 0;
            NSTDExit = false;
            Size = 0;
            Address = 0;
            Offset = -1;
        }

        public CATSInfoSection(Stream stream) : this()
        {
            ReadFromStream(stream);
        }

        public void ReadFromStream (Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            if (!stream.CanRead)
                throw new ArgumentException("stream cannot be read");

            byte[] byteBuffer = new byte[1];

            stream.Read(byteBuffer, 0, 1);
            SectionType = byteBuffer[0];

            stream.Read(byteBuffer, 0, 1);
            NSTDExit = byteBuffer[0] != 0;

            Size = stream.ReadUnsignedShortLE();
            Address = stream.ReadIntLE();

            if (NSTDExit)
                Offset = stream.ReadIntLE();
        }
    }
}
