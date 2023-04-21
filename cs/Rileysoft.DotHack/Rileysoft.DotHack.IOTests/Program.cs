// See https://aka.ms/new-console-template for more information
using Rileysoft.DotHack.FileFormats.CNF;

Console.WriteLine("IO Tests");

Console.WriteLine("Test 1 - system.cnf write/read");
Console.WriteLine("");

if (File.Exists("system.cnf"))
{
    File.Delete("system.cnf");
    Console.WriteLine("Deleted old system.cnf file");
}

CnfFile cnfFile = new CnfFile();
cnfFile.Data.BOOT2 = "Test";
cnfFile.Data.VER = "1.0";
cnfFile.Data.VMODE = "NTSC";

cnfFile.SerializeToPath("system.cnf");

if (!File.Exists("system.cnf"))
{
    Console.WriteLine("Did not find system.cnf!");
}

CnfFile cnfFileRead = new CnfFile("system.cnf", true);

if (cnfFileRead.Data.BOOT2 == "Test" &&
    cnfFileRead.Data.VER == "1.0" &&
    cnfFileRead.Data.VMODE == "NTSC" &&
    cnfFileRead.Data.PARAM2 == null &&
    cnfFileRead.Data.PARAM4 == null)
{
    Console.WriteLine("OK!");
}
else
{
    Console.WriteLine("Mismatched system.cnf");
}

Console.WriteLine("");