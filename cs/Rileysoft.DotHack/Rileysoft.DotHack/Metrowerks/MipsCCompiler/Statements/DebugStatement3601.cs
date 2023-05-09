using Rileysoft.DotHack.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rileysoft.DotHack.Metrowerks.MipsCCompiler.Statements
{
    public class DebugStatement3601 : DebugStatement
    {
        public int CompilerType { get; set; }

        public DebugStatement3601(Stream stream) : base(0x136)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            CompilerType = stream.ReadIntLE();
        }
    }
}
