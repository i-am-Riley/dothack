using Rileysoft.DotHack.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rileysoft.DotHack.Metrowerks.MipsCCompiler.Statements
{
    public class DebugStatement1200 : DebugStatement
    {
        public int Num1 { get; set; }

        public DebugStatement1200(Stream stream) : base(0x12)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            Num1 = stream.ReadIntLE();
        }
    }
}
