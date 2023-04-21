using Rileysoft.DotHack.Metrowerks.MipsCCompiler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Rileysoft.DotHack.Tests.Metrowerks.MipsCCompiler
{
    [TestClass] 
    public class DebugSymbolTests
    {
        [DataTestMethod]
        [DataRow("crt0.s", "Metrowerks MW GAS R5900 Assembler", "D:\\usr\\RpgUS\\prog\\source\\crt0.s")]
        public void Deserialize_Inputs_ReturnCorrectResults (string symbolFile, string compiler, string file)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = $"Rileysoft.DotHack.Tests.Metrowerks.MipsCCompiler.Symbols.{symbolFile}.sym";

            byte[] symbolData = new byte[0];

            using (Stream? stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                    throw new Exception();

                byte[] ba = new byte[stream.Length];
                stream.Read(ba, 0, ba.Length);
                symbolData = ba;
            }

            DebugSymbol sym = new DebugSymbol(symbolData);
            Assert.AreEqual(compiler, sym.GetCompiler());
            Assert.AreEqual(file, sym.GetFile());
        }
    }
}
