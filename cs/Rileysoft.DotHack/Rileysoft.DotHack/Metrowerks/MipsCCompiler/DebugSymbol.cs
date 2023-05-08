﻿using Rileysoft.DotHack.Extensions;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace Rileysoft.DotHack.Metrowerks.MipsCCompiler
{
    public class DebugSymbolField
    {
        public const ushort TypeString = 0x0038;
        public const ushort TypeCompiler = 0x0258;

        public ushort FieldType { get; set; }
        public string VCompiler { get; set; } = "";
        public string VString { get; set; } = "";

        public DebugSymbolField() { }

        public DebugSymbolField(Stream stream)
        {
            Deserialize(stream);
        }

        public string FieldTypeName
        {
            get
            {
                switch (FieldType)
                {
                    case TypeCompiler:
                        return "Compiler String";
                    case TypeString:
                        return "String";
                    default:
                        return $"Unknown (0x{FieldType:X4})";
                }
            }
        }

        public void Deserialize(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            FieldType = stream.ReadUnsignedShortLE();
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
        public const ushort MagicHeader = 0x0136;
        public const ushort CompiledAssembly = 0x8000;
        public const ushort CompiledC = 0x0004;

        public long StreamSize { get; set; }
        public long StartPos { get; set; }
        public long EndPos { get; set; }
        public uint Offset { get; set; }
        public ushort MagicHeaderValue { get; set; }
        public ushort CompiledType { get; set; }
        public string CompiledTypeName
        {
            get
            {
                switch (CompiledType)
                {
                    case CompiledAssembly:
                        return "R5900";
                    case CompiledC:
                        return "C/C++";
                    default:
                        return "Unknown";
                }
            }
        }
        public ushort Unknown1 { get; set; }
        public List<DebugSymbolField> Fields { get; set; } = new List<DebugSymbolField>();
        public int Unknown2 { get; set; }
        public short Unknown3 { get; set; }
        public short Unknown4 { get; set; }
        public IntPtr Unknown5 { get; set; }
        public short Unknown6 { get; set; }
        

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

        public DebugSymbol(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            Deserialize(stream);
        }

        public void Deserialize (Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            StreamSize = stream.Length;
            StartPos = stream.Position;

            Offset = stream.ReadUnsignedIntLE();
            MagicHeaderValue = stream.ReadUnsignedShortLE();
            Debug.WriteLineIf(MagicHeaderValue != MagicHeader, $"Magic Header Value is different:\nExpected: {MagicHeader:X4}\nGot: {MagicHeaderValue:X4}");

            CompiledType = stream.ReadUnsignedShortLE();
            Unknown1 = stream.ReadUnsignedShortLE();
            
            Fields.Add(new DebugSymbolField(stream)); // Compiler
            Fields.Add(new DebugSymbolField(stream)); // Compiled File Path

            Unknown2 = stream.ReadIntLE();
            Unknown3 = stream.ReadShortLE();
            Unknown4 = stream.ReadShortLE();
            Unknown5 = stream.ReadIntPtrLE();
            Unknown6 = stream.ReadShortLE();

            EndPos = stream.Position;
        }
    }
}