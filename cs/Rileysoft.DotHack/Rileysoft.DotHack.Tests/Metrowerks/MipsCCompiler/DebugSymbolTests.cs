using Rileysoft.DotHack.Extensions;
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
            Assert.IsTrue(sym.Fields.Count >= 2);
            Assert.AreEqual(DebugSymbolField.TypeCompiler, sym.Fields[0].FieldType);
            Assert.AreEqual(DebugSymbolField.TypeString, sym.Fields[1].FieldType);
            Assert.AreEqual(compiler, sym.Fields[0].VCompiler);
            Assert.AreEqual(file, sym.Fields[1].VString);

            Debug.WriteLine($"{symbolFile} - {sym.Offset:X8}");
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
                Assert.Inconclusive($"{symbolFile}\n{sym.Offset:X8}");
            }
            else
            {
                Assert.AreEqual((uint)loadedOffset, sym.Offset, $"\n{symbolFile}\nexpected {loadedOffset:X8}\ngot {sym.Offset:X8}");
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
            Assert.AreEqual((uint)stype, sym.CompiledType, $"\n{symbolFile}\nexp: {stype:X4}\nactual: {sym.CompiledType:X4}");
        }

        [DataTestMethod]
        [DataRow("crt0.s")]
        [DataRow("gcc_wrapper.c.1")]
        [DataRow("gcc_wrapper.c._f_ulltof")]
        [DataRow("gcc_wrapper.c._dpflt")]
        [DataRow("gcc_wrapper.c._dpfgt")]
        [DataRow("gcc_wrapper.c._dpfge")]
        [DataRow("mwUtils_PS2.c.1")]
        [DataRow("mwUtils_PS2.c.mwInit")]
        [DataRow("mwUtils_PS2.c.mwBload")]
        [DataRow("mwUtils_PS2.c.mwLoadOverlay")]
        [DataRow("system.cpp.1")]
        [DataRow("sysmem.cpp.1")]
        [DataRow("sysmem.cpp.SearchFree")]
        [DataRow("sysmem.cpp.AddTbl")]
        [DataRow("sysmem.cpp._ccMalloc")]
        [DataRow("sysmem.cpp._ccFree")]
        [DataRow("sysmem.cpp.ccMemReduce")]
        [DataRow("sysmem.cpp.ccMalloc")]
        public void DebugOutput (string symbolFile)
        {
            DebugSymbol sym = GetDebugSymbol(symbolFile);

            Debug.WriteLine($"Symbol File: {symbolFile}");
            Debug.WriteLine($"Symbol Stream Size: {sym.StreamSize} ({sym.StreamSize:X8})");
            Debug.WriteLine($"Symbol Stream StartPos: {sym.StartPos} ({sym.StartPos:X8})");
            Debug.WriteLine($"Symbol Stream EndPos: {sym.EndPos} ({sym.EndPos:X8})\n");

            Debug.WriteLine($"Offset: 0x{sym.Offset.ToStringHexLE()}");
            Debug.WriteLine($"Magic Header: {sym.MagicHeaderValue.ToStringHexLE()}");
            Debug.WriteLine($"Compiled Type: {sym.CompiledType.ToStringHexLE()}");
            Debug.WriteLine($"Compiled Type Name: {sym.CompiledTypeName}");
            Debug.WriteLine($"{nameof(sym.Unknown1)}: {sym.Unknown1.ToStringHexLE()} - {sym.Unknown1}");
            Debug.WriteLine($"{nameof(sym.Unknown2)}: {sym.Unknown2.ToStringHexLE()} - {sym.Unknown2}");
            Debug.WriteLine($"{nameof(sym.Unknown3)}: {sym.Unknown3.ToStringHexLE()} - {sym.Unknown3}");
            Debug.WriteLine($"{nameof(sym.Unknown4)}: {sym.Unknown4.ToStringHexLE()} - {sym.Unknown4}");
            Debug.WriteLine($"{nameof(sym.Unknown5)}: {sym.Unknown5.ToInt32().ToStringHexLE()} - {sym.Unknown5.ToInt32()}");
            Debug.WriteLine($"{nameof(sym.Unknown6)}: {sym.Unknown6.ToStringHexLE()} - {sym.Unknown6}");
            Debug.WriteLine($"Fields (num: {sym.Fields.Count})");
            for (int i=0; i<sym.Fields.Count; i++)
            {
                var field = sym.Fields[i];
                Debug.WriteLine($"[{i}]");
                DebugOutputField(field);
            }
        }

        private void DebugOutputField (DebugSymbolField field)
        {
            Debug.WriteLine($"  Field Type: {field.FieldType.ToStringHexLE()}");
            Debug.WriteLine($"  Field Type Name: {field.FieldTypeName}");

            switch (field.FieldType)
            {
                case DebugSymbolField.TypeCompiler:
                    Debug.WriteLine($"  Value: {field.VCompiler}");
                    break;
                case DebugSymbolField.TypeString:
                    Debug.WriteLine($"  Value: {field.VString}");
                    break;
                default:
                    Debug.WriteLine("  Unknown Value");
                    break;
            }
        }
    }
}
