using Rileysoft.DotHack.Metrowerks.MipsCCompiler;
using System.Diagnostics;
using System.Reflection;

namespace Rileysoft.DotHack.Tests.Metrowerks.MipsCCompiler
{
    [TestClass]
    public class DebugSymbolTests
    {

        private DebugSymbol GetDebugSymbol (string symbolFile)
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
            return sym;
        }

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
        public void Deserialize_Inputs_ReturnsBasicResults(string symbolFile, string compiler, string file)
        {
            DebugSymbol sym = GetDebugSymbol(symbolFile);

            Assert.AreEqual(compiler, sym.GetCompiler());
            Assert.AreEqual(file, sym.GetFile());

            Debug.WriteLine($"{symbolFile} - {sym.GetOffset():X8}");
        }

        [DataTestMethod]
        [DataRow("crt0.s", 0)]
        [DataRow("gcc_wrapper.c.1", -1)]
        [DataRow("gcc_wrapper.c._f_ulltof", 0xD0)]
        [DataRow("gcc_wrapper.c._dpflt", 0x110)]
        [DataRow("gcc_wrapper.c._dpfgt", 0x130)]
        [DataRow("gcc_wrapper.c._dpfge", 0x150)]
        [DataRow("mwUtils_PS2.c.1", -1)]
        [DataRow("mwUtils_PS2.c.mwInit", 0x180)]
        [DataRow("mwUtils_PS2.c.mwBload", 0x1C0)]
        [DataRow("mwUtils_PS2.c.mwLoadOverlay", 0x2C0)]
        [DataRow("system.cpp.1", -1)]
        [DataRow("sysmem.cpp.1", -1)]
        [DataRow("sysmem.cpp.SearchFree", 0x500)]
        [DataRow("sysmem.cpp.AddTbl", 0x5A0)]
        [DataRow("sysmem.cpp._ccMalloc",0x6C0)]
        [DataRow("sysmem.cpp._ccFree", 0x960)]
        [DataRow("sysmem.cpp.ccMemReduce", 0xB00)]
        [DataRow("sysmem.cpp.ccMalloc", 0xC20)]
        public void Deserialize_Inputs_ReturnsCorrectOffset(string symbolFile, int loadedOffset)
        {
            DebugSymbol sym = GetDebugSymbol(symbolFile);

            if (loadedOffset == -1)
            {
                Assert.Inconclusive($"{symbolFile}\n{sym.GetOffset():X8}");
            }
            else
            {
                Assert.AreEqual((uint)loadedOffset, sym.GetOffset(), $"\n{symbolFile}\nexpected {loadedOffset:X8}\ngot {sym.GetOffset():X8}");
            }
        }

        [DataTestMethod]
        [DataRow("crt0.s", 0x8000)]
        [DataRow("gcc_wrapper.c.1", 0x04)]
        [DataRow("gcc_wrapper.c._f_ulltof", 0x04)]
        [DataRow("gcc_wrapper.c._dpflt", 0x04)]
        [DataRow("gcc_wrapper.c._dpfgt", 0x04)]
        [DataRow("gcc_wrapper.c._dpfge", 0x04)]
        [DataRow("mwUtils_PS2.c.1", 0x04)]
        [DataRow("mwUtils_PS2.c.mwInit", 0x04)]
        [DataRow("mwUtils_PS2.c.mwBload", 0x04)]
        [DataRow("mwUtils_PS2.c.mwLoadOverlay", 0x04)]
        [DataRow("system.cpp.1", 0x04)]
        [DataRow("sysmem.cpp.1", 0x04)]
        [DataRow("sysmem.cpp.SearchFree", 0x04)]
        [DataRow("sysmem.cpp.AddTbl", 0x04)]
        [DataRow("sysmem.cpp._ccMalloc", 0x04)]
        [DataRow("sysmem.cpp._ccFree", 0x04)]
        [DataRow("sysmem.cpp.ccMemReduce", 0x04)]
        [DataRow("sysmem.cpp.ccMalloc", 0x04)]
        public void Deserialize_Inputs_ReturnsCorrectSType(string symbolFile, int stype)
        {
            DebugSymbol sym = GetDebugSymbol(symbolFile);
            Assert.AreEqual((uint)stype, sym.GetSType(), $"\n{symbolFile}\nexp: {stype:X4}\nactual: {sym.GetSType():X4}");
        }
    }
}
