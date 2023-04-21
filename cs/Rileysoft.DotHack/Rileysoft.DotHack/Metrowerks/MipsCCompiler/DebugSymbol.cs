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
        private uint m_type;
        

        public DebugSymbol()
        {
            m_magic_main = 0;
            m_magic_compiler = 0;
            m_compiler = "";
            m_magic_file = 0;
            m_file = "";
            m_offset = 0;
            m_type = 0;
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
        
        public void Deserialize(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                Deserialize(ms);
            }
        }

        private static ushort ReadUnsignedShort(byte[] bytes, int offset = 0)
        {
            return (ushort)(
                (ushort)bytes[offset] + 
                ((ushort)bytes[offset + 1] * (ushort)0x100));
        }

        private static uint ReadUnsignedInt(byte[] bytes, int offset = 0)
        {
            return (uint)(
                (uint)bytes[offset] + 
                ((uint)bytes[offset + 1] * (uint)0x100) + 
                ((uint)bytes[offset + 2] * (uint)0x10000) + 
                ((uint)bytes[offset + 3] * (uint)0x1000000));
        }

        private static string ReadCString(Stream stream)
        {
            char[] charBuf = new char[1];
            byte[] readBuf = new byte[1];
            int len = 0;

            while (stream.Read(readBuf, 0, 1) == 1)
            {
                if (readBuf[0] == 0)
                    break;

                if (len == charBuf.Length)
                {
                    char[] cloneBuf = new char[charBuf.Length * 2];
                    for (int i=0; i<len; i++)
                    {
                        cloneBuf[i] = charBuf[i];
                    }
                    charBuf = cloneBuf;
                }

                charBuf[len++] = Convert.ToChar(readBuf[0]);
            }

            return new string(charBuf, 0, len);
        }

        public void Deserialize(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            byte[] buffer = new byte[14];
            stream.Read(buffer, 0, 12);

            m_offset = ReadUnsignedInt(buffer);
            m_magic_main = ReadUnsignedShort(buffer, 4);
            m_type = ReadUnsignedInt(buffer, 6);
            m_magic_compiler = ReadUnsignedShort(buffer, 10);
            if (m_magic_compiler != magic_compiler)
            {
                Debug.WriteLine($"magic compiler prefix mismatch - exp: {magic_compiler:X2} act: {m_magic_compiler:X2}");
            }

            m_compiler = ReadCString(stream);

            stream.Read(buffer, 12, 2);

            m_magic_file = ReadUnsignedShort(buffer, 12);
            if (m_magic_file != magic_file)
            {
                Debug.WriteLine($"magic file prefix mismatch - exp: {magic_file:X2} act: {m_magic_file:X2}");
            }
            
            m_file = ReadCString(stream);
        }
    }
}
#pragma warning restore CA1024 // Use properties where appropriate