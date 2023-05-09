using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rileysoft.DotHack.Metrowerks.MipsCCompiler.Statements
{
    public class DebugStatement
    {
        public ushort Value { get; set; }

        public DebugStatement(ushort value)
        {
            Value = value;
        }
    }
}
