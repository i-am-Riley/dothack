#pragma warning disable CA1707 // Identifiers should not contain underscores
namespace Rileysoft.DotHack.SLUS20267.CodeWarrior.PS2Support
{
    /// <summary>
    /// C:\CodeWarrior\PS2 Support\gcc_wrapper.c
    /// GCC wrappers provide customization for GCC by setting default compiler options, defining macros, or performing other tasks before invoking the actual compiler. 
    /// </summary>
    public static class gcc_wrapper
    {
        /// <summary>
        /// (_f_ulltof) - unsigned long long to float
        /// </summary>
        /// <param name="ul">unsigned long long</param>
        /// <returns>float</returns>
        public static float _ulltof (ulong ul)
        {
            return ul;
        }

        /// <summary>
        /// (_dpflt) - Double precision compare less than
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>true if left is less than right</returns>
        public static bool _dpflt(double left, double right) 
        {
            // allocates 16 bytes for local variables, left and right?
            return dpcmp(left, right) < 0;
        }

        /// <summary>
        /// (_dpfgt) - Double precision compare greater than
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>true if left is greater than right</returns>
        public static bool _dpfgt(double left, double right)
        {
            return dpcmp(left, right) == 1;
        }

        /// <summary>
        /// (_dpfge) - Double precision compare greater than equal to
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>true if left is greater than or equal to right</returns>
        public static bool _dpfge(double left, double right)
        {
            return dpcmp(left, right) >= 0;
        }

        /// <summary>
        /// (dpcmp) - Double precision compare
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>1 if left is greater than right, 0 if left is equal to right, -1 if left is less than right</returns>
        private static int dpcmp (double left, double right)
        {
            if (left > right)
            {
                return 1;
            }

            if (left == right)
            {
                return 0;
            }

            return -1;
        }

    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores