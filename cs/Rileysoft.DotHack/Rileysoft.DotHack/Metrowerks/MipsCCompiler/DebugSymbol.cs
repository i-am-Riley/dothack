using Rileysoft.DotHack.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable CA1024 // Use properties where appropriate
namespace Rileysoft.DotHack.Metrowerks.MipsCCompiler
{
    public class DebugSymbol
    {
        private const ushort magic_main = 0x0136;
        private const ushort magic_compiler = 0x0258;
        private const ushort magic_file = 0x0038;

        private ushort m_magic_main;
        private int m_magic_compiler;
        private string m_compiler;
        private int m_magic_file;
        private string m_file;
        private uint m_offset;
        private uint m_stype;
        

        public DebugSymbol()
        {
            m_magic_main = 0;
            m_magic_compiler = 0;
            m_compiler = "";
            m_magic_file = 0;
            m_file = "";
            m_offset = 0;
            m_stype = 0;
        }

        public DebugSymbol(byte[] data) : this()
        {
            Deserialize(data);
        }

        public DebugSymbol(Stream stream) : this()
        {
            Deserialize(stream);
        }

        public string? GetCompiler ()
        {
            return m_compiler;
        }

        public void SetCompiler (string compiler)
        {
            m_compiler = compiler;
        }

        public string? GetFile ()
        {
            return m_file;
        }

        public void SetFile (string file)
        {
            m_file = file;
        }

        public uint GetOffset ()
        {
            return m_offset;
        }

        public void SetOffset (uint offset)
        {
            m_offset = offset;
        }

        public uint GetSType ()
        {
            return m_stype;
        }

        public void SetSSType (uint stype)
        {
            m_stype = stype;
        }

        public void Deserialize(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                Deserialize(ms);
            }
        }

        public void Deserialize(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            byte[] buffer = new byte[14];
            stream.Read(buffer, 0, 12);

            m_offset = buffer.ReadUnsignedInt();
            m_magic_main = buffer.ReadUnsignedShort(4);
            if (m_magic_main != magic_main)
            {
                Debug.WriteLine($"magic main prefix mismatch - exp: {magic_main:X2} act: {m_magic_main:X2}");
            }

            m_stype = buffer.ReadUnsignedInt(6);
            m_magic_compiler = buffer.ReadUnsignedShort(10);
            if (m_magic_compiler != magic_compiler)
            {
                Debug.WriteLine($"magic compiler prefix mismatch - exp: {magic_compiler:X2} act: {m_magic_compiler:X2}");
            }

            m_compiler = stream.ReadCString();

            stream.Read(buffer, 12, 2);

            m_magic_file = buffer.ReadUnsignedShort(12);
            if (m_magic_file != magic_file)
            {
                Debug.WriteLine($"magic file prefix mismatch - exp: {magic_file:X2} act: {m_magic_file:X2}");
            }
            
            m_file = stream.ReadCString();
        }
    }
}
#pragma warning restore CA1024 // Use properties where appropriate