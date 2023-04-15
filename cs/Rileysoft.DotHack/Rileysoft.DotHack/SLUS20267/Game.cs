using Rileysoft.DotHack.FileFormats.CNF;

namespace Rileysoft.DotHack.SLUS20267
{
    public static class Game
    {
        public const string ID = "SLUS-20267";
        public static readonly CnfData CnfData = new()
        {
            BOOT2 = @"cdrom0:\SLUS_202.67;1",
            VER = "1.00",
            VMODE = "NTSC"
        };

        static Game()
        {
            CnfData.MakeReadonly();
        }
    }
}
