using Rileysoft.DotHack.Extensions;
using Rileysoft.DotHack.Metrowerks.MipsCCompiler.Statements;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rileysoft.DotHack.Metrowerks.MipsCCompiler
{
    public class ElfDebugProcessor
    {
        public List<DebugFile> DebugFiles { get; set; }
        public bool BeenRan { get; private set; }

        public ElfDebugProcessor()
        {
            DebugFiles = new List<DebugFile>();
            BeenRan = false;
        }

        public void ReadFromStream(Stream stream, long length)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            BeenRan = false;
            long end = stream.Position + length;

            DebugFiles.Clear();
            DebugFile currentFile = new DebugFile();
            currentFile.Num1 = stream.ReadIntLE();

            while (stream.Position < end && stream.Position < stream.Length)
            {
                ushort command = stream.ReadUnsignedShortLE();
                switch (command)
                {
                    // ???
                    case 0x0000:
                        var s0000 = new DebugStatement0000(stream);
                        currentFile.Statements.Add(s0000);
                        break;

                    // ???
                    case 0x0007:
                        var s0700 = new DebugStatement0700(stream);
                        currentFile.Statements.Add(s0700);
                        break;

                    // Always at the end of a debug file
                    case 0x0011:
                        var s1100 = new DebugStatement1100(stream);
                        currentFile.Statements.Add(s1100);
                        break;

                    // Always comes after 0x11
                    case 0x0012:
                        var s1200 = new DebugStatement1200(stream);
                        currentFile.Statements.Add(s1200);
                        break;

                    // new symbol
                    case 0x0136:
                        DebugFiles.Add(currentFile);
                        currentFile = new DebugFile();

                        var s3601 = new DebugStatement3601(stream);
                        currentFile.Statements.Add(s3601);
                        break;

                    // compiler name
                    case 0x0258:
                        var s5802 = new DebugStatement5802(stream);
                        currentFile.Statements.Add(s5802);
                        break;

                    // string
                    case 0x0038:
                        var s3800 = new DebugStatement3800(stream);
                        currentFile.Statements.Add(s3800);
                        break;

                    default:
                        // unknown command, stream may become misaligned.
                        var sunknown = new DebugStatement(command);
                        currentFile.Statements.Add(sunknown);
                        DebugFiles.Add(currentFile);
                        throw new NotImplementedException($"Unknown debug command: {command.ToStringHexLE()} @ {stream.Position:X8}");
                }
            }

            DebugFiles.Add(currentFile);
            BeenRan = true;
        }
    }
}
