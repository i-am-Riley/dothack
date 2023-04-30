using Rileysoft.DotHack.Extensions;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace Rileysoft.DotHack.Metrowerks.MipsCCompiler
{
    public class DebugSymbolField
    {
        public const uint TypeString = 0x0038;
        public const uint TypeCompiler = 0x0258;

        public uint FieldType { get; set; }
        public string VCompiler { get; set; } = "";
        public string VString { get; set; } = "";

        public DebugSymbolField() { }

        public DebugSymbolField(Stream stream)
        {
            Deserialize(stream);
        }

        public void Deserialize(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            FieldType = stream.ReadUnsignedShort();
            switch (FieldType)
            {
                case TypeCompiler:
                    VCompiler = stream.ReadCString();
                    break;
                case TypeString:
                    VString = stream.ReadCString();
                    break;
            }
        }
    }
    public class DebugSymbol
    {
        public const uint MagicHeader = 0x0136;
        public const uint CompiledAssembly = 0x0080;
        public const uint CompiledC = 0x0400;
        
        public uint Offset { get; set; }
        public ushort MagicHeaderValue { get; set; }
        public uint CompiledType { get; set; }
        public uint UnknownOne { get; set; }
        public List<DebugSymbolField> Fields { get; set; } = new List<DebugSymbolField>();

        public DebugSymbol() { }

        public DebugSymbol(byte[] bytes, int offset = 0, int count = -1)
        {
            if (bytes == null)
                throw new ArgumentNullException(nameof(bytes));

            if (count == -1)
                count = bytes.Length;
            
            using (MemoryStream ms = new MemoryStream(bytes, offset, count))
            {
                Deserialize(ms);
            }
        }

        public void Deserialize (Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            Offset = stream.ReadUnsignedInt();
            MagicHeaderValue = stream.ReadUnsignedShort();
            Debug.WriteLineIf(MagicHeaderValue != MagicHeader, $"Magic Header Value is different:\nExpected: {MagicHeader:X4}\nGot: {MagicHeaderValue:X4}");

            CompiledType = stream.ReadUnsignedShort();
            UnknownOne = stream.ReadUnsignedShort();
            
            Fields.Add(new DebugSymbolField(stream)); // Compiler
            Fields.Add(new DebugSymbolField(stream)); // Compiled File Path
        }
    }
}