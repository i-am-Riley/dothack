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
        public static void _dpflt(double left, double right) { } // left, right
        public static void _dpfgt() { } // left, right
        public static void _dpfge() { } // left, right

    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores