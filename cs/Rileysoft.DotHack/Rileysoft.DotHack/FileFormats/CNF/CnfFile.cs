using System.Text;

namespace Rileysoft.DotHack.FileFormats.CNF
{
    /// <summary>
    /// See https://www.psdevwiki.com/ps2/System.cnf
    /// </summary>
    public class CnfFile
    {
        private const string ReadonlyError = "Object is readonly";
        private CnfData _Data = new();

        public CnfFile() { }

        public CnfFile(CnfData data) : this()
        {
            _Data = data;
        }

        /// <summary>
        /// Creates a new instance of CnfFile and if makeReadonly is set to true, makes this and the passed data readonly.
        /// </summary>
        public CnfFile(CnfData data, bool makeReadonly = false) : this(data)
        {
            if (makeReadonly)
                MakeReadonly();
        }

        /// <summary>
        /// Reads a CnfFile from the specified path
        /// </summary>
        public CnfFile(string path, bool makeReadonly = false)
        {
            DeserializeFromPath(path);

            if (makeReadonly)
                MakeReadonly();
        }

        /// <summary>
        /// Makes this object and the underlying Data object readonly
        /// </summary>
        public void MakeReadonly () 
        { 
            _Data.MakeReadonly();
        }

        /// <summary>
        /// Is this object readonly?
        /// </summary>
        public bool Readonly => _Data.Readonly;

        /// <summary>
        /// <see cref="CnfData"/> object
        /// See https://www.psdevwiki.com/ps2/System.cnf
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when this property is assigned while the object is readonly.</exception>
        public CnfData Data
        {
            get => _Data;
            set
            {
                if (Readonly)
                    throw new InvalidOperationException(ReadonlyError);

                _Data = value;
            }
        }

        /// <summary>
        /// Serializes the data from this object into the CNF file format
        /// </summary>
        public byte[] Serialize()
        {
            string output = @"";

            if (Data.BOOT2 is not null)
                output += $"BOOT2 = {Data.BOOT2}\r\n";

            if (Data.VER is not null)
                output += $"VER = {Data.VER}\r\n";

            if (Data.VMODE is not null)
                output += $"VMODE = {Data.VMODE}\r\n";

            if (Data.PARAM2 is not null)
                output += $"PARAM2 = {Data.PARAM2}\r\n";

            if (Data.PARAM4 is not null)
                output += $"PARAM4 = {Data.PARAM4}\r\n";

            return Encoding.ASCII.GetBytes(output);
        }

        /// <summary>
        /// Serializes the data from this object into the CNF file format and writes it to the specified path
        /// </summary>
        public void SerializeToPath (string path)
        {
            File.WriteAllBytes(path, Serialize());
        }

        /// <summary>
        /// Deserializes CNF data from a byte array
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when this object is readonly</exception>
        /// <exception cref="InvalidDataException">Thrown when the provided input data is invalid</exception>
        public void Deserialize (byte[] input)
        {
            if (Readonly)
                throw new InvalidOperationException(ReadonlyError);

            Deserialize(Encoding.ASCII.GetString(input));
        }

        /// <summary>
        /// Deserializes CNF data from a string
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when this object is readonly</exception>
        /// <exception cref="InvalidDataException">Thrown when the provided input data is invalid</exception>
        public void Deserialize (string input)
        {
            if (Readonly)
                throw new InvalidOperationException(ReadonlyError);

            string[] settings = input.Split("\r\n");
            foreach (string setting in settings)
            {
                if (setting.Length == 0)
                    continue;

                int keyEndingIndex = setting.IndexOf(" = ");
                if (keyEndingIndex == -1)
                    throw new InvalidDataException($"Cannot read CNF data, no key/value pair found for input: {setting}");

                string key = setting.Substring(0, keyEndingIndex);
                string value = setting.Substring(keyEndingIndex + 3);

                if (key == "BOOT2")
                    Data.BOOT2 = value;

                if (key == "VER")
                    Data.VER = value;

                if (key == "VMODE")
                    Data.VMODE = value;

                if (key == "PARAM2")
                    Data.PARAM2 = value;

                if (key == "PARAM4")
                    Data.PARAM4 = value;
            }
        }

        /// <summary>
        /// Deserializes CNF data from a file
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when this object is readonly</exception>
        /// /// <exception cref="InvalidDataException">Thrown when the provided input data is invalid</exception>
        public void DeserializeFromPath (string path)
        {
            if (Readonly)
                throw new InvalidOperationException(ReadonlyError);

            byte[] data = File.ReadAllBytes(path);
            Deserialize(Encoding.ASCII.GetString(data));
        }
    }
}
