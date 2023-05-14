namespace Rileysoft.DotHack.FileFormats.ELF
{
    [Flags]
    public enum ELFPFLAGS : uint
    {
        /// <summary>
        /// An executable segment.
        /// </summary>
        PF_X = 1,

        /// <summary>
        /// A writable segment.
        /// </summary>
        PF_W = 2,

        /// <summary>
        /// A readable segment.
        /// </summary>
        PF_R = 4
    }


}
