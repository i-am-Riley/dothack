using Rileysoft.DotHack.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rileysoft.DotHack.Metrowerks.MipsCCompiler.Statements
{
    public class DebugStatement3800 : DebugStatement
    {
        public string StringValue { get; set; }

        public DebugStatement3800 (Stream stream) : base(0x38)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            StringValue = stream.ReadCString();
        }
    }
}
