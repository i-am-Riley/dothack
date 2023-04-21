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
        [DataRow("gcc_wrapper.c.1", "MW MIPS C Compiler", "C:\\CodeWarrior\\PS2 Support\\gcc_wrapper.c")]
        [DataRow("gcc_wrapper.c._f_ulltof", "MW MIPS C Compiler", "C:\\CodeWarrior\\PS2 Support\\gcc_wrapper.c")]
        [DataRow("gcc_wrapper.c._dpflt", "MW MIPS C Compiler", "C:\\CodeWarrior\\PS2 Support\\gcc_wrapper.c")]
        [DataRow("gcc_wrapper.c._dpfgt", "MW MIPS C Compiler", "C:\\CodeWarrior\\PS2 Support\\gcc_wrapper.c")]
        [DataRow("gcc_wrapper.c._dpfge", "MW MIPS C Compiler", "C:\\CodeWarrior\\PS2 Support\\gcc_wrapper.c")]
        [DataRow("mwUtils_PS2.c.1", "MW MIPS C Compiler", "D:\\usr\\RpgUS\\prog\\source\\mwUtils_PS2.c")]
        [DataRow("mwUtils_PS2.c.mwInit", "MW MIPS C Compiler", "D:\\usr\\RpgUS\\prog\\source\\mwUtils_PS2.c")]
        [DataRow("mwUtils_PS2.c.mwBload", "MW MIPS C Compiler", "D:\\usr\\RpgUS\\prog\\source\\mwUtils_PS2.c")]
        [DataRow("mwUtils_PS2.c.mwLoadOverlay", "MW MIPS C Compiler", "D:\\usr\\RpgUS\\prog\\source\\mwUtils_PS2.c")]
        [DataRow("system.cpp.1", "MW MIPS C Compiler", "D:\\usr\\RpgUS\\prog\\system\\system.cpp")]
        [DataRow("sysmem.cpp.1", "MW MIPS C Compiler", "D:\\usr\\RpgUS\\prog\\system\\sysmem.cpp")]
        [DataRow("sysmem.cpp.SearchFree", "MW MIPS C Compiler", "D:\\usr\\RpgUS\\prog\\system\\sysmem.cpp")]
        [DataRow("sysmem.cpp.AddTbl", "MW MIPS C Compiler", "D:\\usr\\RpgUS\\prog\\system\\sysmem.cpp")]
        [DataRow("sysmem.cpp._ccMalloc", "MW MIPS C Compiler", "D:\\usr\\RpgUS\\prog\\system\\sysmem.cpp")]
        [DataRow("sysmem.cpp._ccFree", "MW MIPS C Compiler", "D:\\usr\\RpgUS\\prog\\system\\sysmem.cpp")]
        [DataRow("sysmem.cpp.ccMemReduce", "MW MIPS C Compiler", "D:\\usr\\RpgUS\\prog\\system\\sysmem.cpp")]
        [DataRow("sysmem.cpp.ccMalloc", "MW MIPS C Compiler", "D:\\usr\\RpgUS\\prog\\system\\sysmem.cpp")]
        public void Deserialize_Inputs_ReturnsBasicResults (string symbolFile, string compiler, string file)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = $"Rileysoft.DotHack.Tests.Metrowerks.MipsCCompiler.Symbols.{symbolFile}.sym";

            byte[] symbolData = new byte[0];

            using (Stream? stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                    throw new Exception("file not found");

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
