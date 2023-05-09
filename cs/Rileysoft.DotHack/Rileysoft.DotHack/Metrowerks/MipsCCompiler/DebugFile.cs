using Rileysoft.DotHack.Metrowerks.MipsCCompiler.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rileysoft.DotHack.Metrowerks.MipsCCompiler
{
    public class DebugFile
    {
        public int Num1 { get; set; }
        public int CompilerType { get; set; }

        public List<DebugStatement> Statements { get; set; }

        public DebugFile()
        {
            Statements = new List<DebugStatement>();
        }
    }
}
