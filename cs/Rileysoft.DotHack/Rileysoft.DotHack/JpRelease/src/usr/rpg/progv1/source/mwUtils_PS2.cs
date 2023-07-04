namespace Rileysoft.DotHack.JpRelease.src.usr.rpg.progv1.source
{
    public class mwUtils_PS2
    {
        public static ExceptionTableIndex[] __exception_table_end__ = new ExceptionTableIndex[0]; // user defined type 0x96f
        public static ExceptionTableIndex[] __exception_table_start__ = new ExceptionTableIndex[0]; // user defined type 0x949
        public delegate void Delegate755();
        public static Delegate755 __static_init_end; // user defined type 0x9be

        //AT_subscr_data(<21> FMT_FT_C_C: FT_long [0:-1] FMT_ET: AT_mod_u_d_type(<5>MOD_pointer_to (00000755)))
        // not sure if this is the correct interpretation.
        // according to https://github.com/Palm-Studios/sh2_source/blob/main/Program%20Files/Metrowerks/CodeWarrior/PS2%20Support/Runtime/Sources/CPP_Support/mwUtils_PS2.c
        // void(*__static_init)()[0];
        public static Delegate755 __static_init; // user defined type 0x995

        // these are local variables for some scope.
        public int mwHdlrID_VIF0;
        public int mwHdlrID_VIF1;
        public int mwHdlrState_VIF0;
        public int mwHdlrState_VIF1;

        public static int mwDbgVifHandler => 0; // mwDbgVifHandler__Fi user defined type 0xa5d

        public class mwOverlayHeader
        {
            public byte[] identifier = new byte[3]; // user defined type 0x901
            public char[]? version;
            public uint id;
            public uint address;
            public uint sz_text;
            public uint sz_data;
            public uint sz_bss;
            public uint _static_init;
            public uint _static_init_end;
            public byte[] name = new byte[32]; // user defined type 0x925
        }

        public class tVIF_MARK
        {
            private uint value;

            public uint MARK
            {
                get
                {
                    return (ushort)(value & 0x0000FFFF); // get the lower 16 bits
                }
                set
                {
                    this.value = (uint)((this.value & 0xFFFF0000) | (value & 0x0000FFFF)); // set the lower 16 bits
                }
            }

            public uint p0
            {
                get
                {
                    return (ushort)((value & 0xFFFF0000) >> 16); // get the upper 16 bits
                }
                set
                {
                    this.value = (uint)((this.value & 0x0000FFFF) | ((value & 0x0000FFFF) << 16)); // set the upper 16 bits
                }
            }
        }

        public delegate int DelegateA81(int a);

        public class ExceptionTableIndex
        {
            public IntPtr function_address;
            public uint function_size;
            public IntPtr exception_table;
        }

        public void mwInit()
        {
            // calls globals: __exception_table_end__, __exception_table_start__, __static_init_end, __static_init
        }

        public int mwBload(PointerTo<char> pFilePath, PointerTo<Void> pAddress)
        {
            int size2 = 0;
            int size = 0;
            PointerTo<Void>? pBuffer = null;
            int rfd = 0;

            return rfd;
        }

        public int mwLoadOverlay(PointerTo<char> pFilePath, PointerTo<Void> pAddress)
        {
            int result = 0;
            int size = 0;

            return result;
        }
    }
}
