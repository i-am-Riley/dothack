using Rileysoft.DotHack.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rileysoft.DotHack.Metrowerks.MipsCCompiler.Statements
{
    public class DebugStatement0000 : DebugStatement
    {
        public int Num1 { get; set; }

        public DebugStatement0000 (Stream stream) : base(0x00)
        {
            Num1 = stream.ReadIntLE();
        }
    }
}
