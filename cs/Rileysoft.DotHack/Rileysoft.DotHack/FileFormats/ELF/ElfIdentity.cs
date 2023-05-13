using Rileysoft.DotHack.Extensions;
using System.Diagnostics;

#pragma warning disable CA1707

namespace Rileysoft.DotHack.FileFormats.ELF
{
    public class ElfIdentity
    {
        public const byte ELFMAG0 = 0x7F;
        public const byte ELFMAG1 = 0x45;
        public const byte ELFMAG2 = 0x4C;
        public const byte ELFMAG3 = 0x46;

        /// <summary>
        /// The first byte of the magic number.  It must be
        /// filled with ELFMAG0.  (0: 0x7f)
        /// </summary>
        public byte EI_MAG0 { get; set; }

        /// <summary>
        /// The second byte of the magic number.  It must be
        /// filled with ELFMAG1.  (1: 'E')
        /// </summary>
        public byte EI_MAG1 { get; set; }

        /// <summary>
        /// The third byte of the magic number.  It must be
        /// filled with ELFMAG2.  (2: 'L')
        /// </summary>
        public byte EI_MAG2 { get; set; }

        /// <summary>
        /// The fourth byte of the magic number.  It must be
        /// filled with ELFMAG3.  (3: 'F')
        /// </summary>
        public byte EI_MAG3 { get; set; }

        /// <summary>
        /// The fifth byte identifies the architecture for this binary
        /// </summary>
        public ELFCLASS EI_CLASS { get; set; } = ELFCLASS.ELFCLASSNONE;

        /// <summary>
        /// The sixth byte specifies the data encoding of the
        /// processor-specific data in the file.
        /// </summary>
        public ELFDATA EI_DATA { get; set; } = ELFDATA.ELFDATANONE;

        /// <summary>
        /// The seventh byte is the version number of the ELF
        /// specification
        /// </summary>
        public ELFVERSION EI_VERSION { get; set; } = ELFVERSION.EV_NONE;

        /// <summary>
        /// The eighth byte identifies the operating system and
        /// ABI to which the object is targeted.  Some fields
        /// in other ELF structures have flags and values that
        /// have platform-specific meanings; the interpretation
        /// of those fields is determined by the value of this
        /// byte.
        /// </summary>
        public ELFOSABI EI_OSABI { get; set; } = ELFOSABI.ELFOSABI_NONE;

        /// <summary>
        /// The ninth byte identifies the version of the ABI to
        /// which the object is targeted.  This field is used
        /// to distinguish among incompatible versions of an
        /// ABI.  The interpretation of this version number is
        /// dependent on the ABI identified by the EI_OSABI
        /// field.  Applications conforming to this
        /// specification use the value 0.
        /// </summary>
        public byte EI_ABIVERSION { get; set; }
        public bool HeaderMismatch { get; private set; }

        public void ReadFromStream(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            byte[] data = new byte[16];
            stream.Read(data, 0, 16);

            EI_MAG0 = data[0];
            EI_MAG1 = data[1];
            EI_MAG2 = data[2];
            EI_MAG3 = data[3];

            bool HeaderMismatch =
                (EI_MAG0 != ELFMAG0) ||
                (EI_MAG1 != ELFMAG1) ||
                (EI_MAG2 != ELFMAG2) ||
                (EI_MAG3 != ELFMAG3);

            if (HeaderMismatch)
                Debug.WriteLine($"Header mismatched: \nGot: {data.ToStringHexExpanded(0, 4)}");

            EI_CLASS = (ELFCLASS)data[4];
            EI_DATA = (ELFDATA)data[5];
            EI_VERSION = (ELFVERSION)data[6];
            EI_OSABI = (ELFOSABI)data[7];
            EI_ABIVERSION = data[8];
        }
    }
}

#pragma warning restore CA1707
