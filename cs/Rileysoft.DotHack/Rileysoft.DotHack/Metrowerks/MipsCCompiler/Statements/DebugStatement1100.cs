using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rileysoft.DotHack.Metrowerks.MipsCCompiler.Statements
{
    public class DebugStatement1100 : DebugStatement
    {
        public DebugStatement1100(Stream stream) : base(0x11)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

        }
    }
}
