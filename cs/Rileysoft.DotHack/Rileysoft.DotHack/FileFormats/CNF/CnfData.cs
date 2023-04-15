namespace Rileysoft.DotHack.FileFormats.CNF
{
    /// <summary>
    /// See https://www.psdevwiki.com/ps2/System.cnf
    /// </summary>
    public class CnfData
    {
        private const string ReadonlyError = "Object is readonly";
        private string? _BOOT2 = null;
        private string? _VER = null;
        private string? _VMODE = null;
        private string? _PARAM2 = null;
        private string? _PARAM4 = null;
        private bool _Readonly = false;

        /// <summary>
        /// Full path to the executable to launch
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when this property is assigned while the object is readonly.</exception>
        public string? BOOT2
        {
            get => _BOOT2;
            set
            {
                if (_Readonly)
                    throw new InvalidOperationException(ReadonlyError);

                _BOOT2 = value;
            }
        }

        /// <summary>
        /// Title version
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when this property is assigned while the object is readonly.</exception>
        public string? VER
        {
            get => _VER;
            set
            {
                if (_Readonly)
                    throw new InvalidOperationException(ReadonlyError);

                _VER = value;
            }
        }

        /// <summary>
        /// Video Mode, PAL or NTSC
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when this property is assigned while the object is readonly.</exception>
        public string? VMODE
        {
            get => _VMODE;
            set
            {
                if (_Readonly)
                    throw new InvalidOperationException(ReadonlyError);

                _VMODE = value;
            }

        }

        /// <summary>
        /// Settings for Deckard PS2 model
        /// See https://www.psdevwiki.com/ps2/System.cnf
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when this property is assigned while the object is readonly.</exception>
        public string? PARAM2
        {
            get => _PARAM2;
            set
            {
                if (_Readonly)
                    throw new InvalidOperationException(ReadonlyError);

                _PARAM2 = value;
            }
        }

        /// <summary>
        /// See https://www.psdevwiki.com/ps3/PS2_Emulation#TitleID.2FDiscID_in_ps2_netemu.self
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when this property is assigned while the object is readonly.</exception>
        public string? PARAM4
        {
            get => _PARAM4;
            set
            {
                if (_Readonly)
                    throw new InvalidOperationException(ReadonlyError);

                _PARAM4 = value;
            }
        }

        /// <summary>
        /// Sets this object as readonly
        /// </summary>
        public void MakeReadonly()
        {
            _Readonly = true;
        }

        /// <summary>
        /// Is this object readonly?
        /// </summary>
        public bool Readonly => _Readonly;
    }
}
