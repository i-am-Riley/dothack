using Rileysoft.Common.Extensions;

namespace Rileysoft.DotHack.Metrowerks.MipsCCompiler
{
    public class ElfDebugProcessor
    {
        public bool BeenRan { get; private set; }

        public ElfDebugProcessor()
        {
            BeenRan = false;
        }

        public void ReadFromStream(Stream stream, long length)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            BeenRan = false;
            long end = stream.Position + length;

            int Num1 = stream.ReadIntLE();

            while (stream.Position < end && stream.Position < stream.Length)
            {
                ushort command = stream.ReadUnsignedShortLE();
                switch (command)
                {
                    // ???
                    case 0x0000:
                        break;

                    // ???
                    case 0x0007:
                        break;

                    // Always at the end of a debug file
                    case 0x0011:
                        break;

                    // Always comes after 0x11
                    case 0x0012:
                        break;

                    // new symbol
                    case 0x0136:
                        break;

                    // compiler name
                    case 0x0258:
                        break;

                    // string
                    case 0x0038:
                        break;

                    default:
                        // unknown command, stream may become misaligned.
                        throw new NotImplementedException($"Unknown debug command: {command.ToStringHexLE()} @ {stream.Position:X8}");
                }
            }

            BeenRan = true;
        }
    }
}
