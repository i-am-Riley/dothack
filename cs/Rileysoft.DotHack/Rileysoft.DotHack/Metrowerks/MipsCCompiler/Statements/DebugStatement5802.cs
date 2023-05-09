using Rileysoft.DotHack.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rileysoft.DotHack.Metrowerks.MipsCCompiler.Statements
{
    public class DebugStatement5802 : DebugStatement
    {
        public string Compiler { get; set; }

        public DebugStatement5802(Stream stream) : base(0x258)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            Compiler = stream.ReadCString();
        }
    }
}
