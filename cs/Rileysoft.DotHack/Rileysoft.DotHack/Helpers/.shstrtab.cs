using Rileysoft.DotHack.Extensions;

namespace Rileysoft.DotHack.Helpers
{
    public static class _shstrtab
    {
        public static readonly byte[] Header = new byte[]
        {
            0x00, 0x2E, 0x73, 0x68, 0x73, 0x74, 0x72, 0x74, 0x61, 0x62, 0x00
        };

        public static List<string> ReadFromStream (Stream stream, bool seek = false, long offset = 0, SeekOrigin origin = SeekOrigin.Begin)
        {
            // x278980
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            if (seek && !stream.CanSeek)
                throw new InvalidOperationException("cannot seek");

            if (seek)
                stream.Seek(offset, origin);

            byte[] header = new byte[Header.Length];
            stream.Read(header, 0, header.Length);

            bool areEqual = true;
            for (int i=0; i<header.Length; i++)
                if (header[i] != Header[i]) 
                { 
                    areEqual = false; 
                    break; 
                }

            if (!areEqual)
                throw new InvalidOperationException("invalid header");

            string lastString;
            List<string> @return = new List<string>();

            while (stream.Position < stream.Length)
            {
                lastString = stream.ReadCString();

                if (lastString.Length == 0)
                    break;

                @return.Add(lastString);
            }

            return @return;
        }
    }
}
