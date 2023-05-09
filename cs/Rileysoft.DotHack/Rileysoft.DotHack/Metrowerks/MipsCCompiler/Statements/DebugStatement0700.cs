using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rileysoft.DotHack.Metrowerks.MipsCCompiler.Statements
{
    public class DebugStatement0700 : DebugStatement
    {
        public byte[] Bytes { get; set; }

        public DebugStatement0700 (Stream stream) : base(0x07)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            Bytes = new byte[3];
            stream.Read(Bytes, 0, 3);
        }

            
    }
}
