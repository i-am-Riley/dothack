namespace Rileysoft.DotHack.JpRelease.src.Program_Files.Metrowerks.CodeWarrior.PS2_Support
{
    //C++
    //MW MIPS C Compiler
    //C:\Program Files\Metrowerks\CodeWarrior\PS2 Support\gcc_wrapper.c
    public static class gcc_wrapper
    {
        // global subroutine 0x1fa sibling | 0x1000d0 low pc | 0x10010c high pc
        // function type: float
        // return addr: <6> OP_BASEREG(29) OP_DREF8
        // restore sp: <11> (OP_REG(29) OP_CONST(16) OP_ADD)
        // name: _f_ulltof

        // formal parameter 0x1b7 sibling
        // unsigned long long | location <5> OP_REG(2) | name ul
        public static float _f_ulltof(ulong ul)
        {
            //float local variable 0x1f2 sibling
            //location: <11> OP_BASEREG(29) OP_CONST(16) OP_ADD
            //name: f
            float f = (float)ul;
            return f;
        }

        public static int _dpflt(double left, double right)
        {
            if (left < right)
            {
                return 1;
            }

            return 0;
        }

        public static int _dpfgt(double left, double right)
        {
            if (right < left)
            {
                return 1;
            }

            return 0;
        }

        public static int _dpfge(double left, double right)
        {
            if (left == right)
                return 1;

            return 0;
        }
    }
}
