using ElfDebugTest;

Console.WriteLine("ELF Debug Test Output");
Console.WriteLine("");

var files = new string[]
{
    "C:\\Users\\Admin\\Desktop\\dothack\\Dot Hack Kansen Kakudai Vol. 1 (Japan)\\Slps_251.21",
    "C:\\Users\\Admin\\Desktop\\dothack\\Dot Hack Part 1 - Infection (USA) (Demo 2)\\bandai.elf",
    "C:\\Users\\Admin\\Desktop\\dothack\\Dot Hack Part 1 - Infection (USA) (En,Ja)\\Slus_202.67",
    //"C:\\Users\\Admin\\Desktop\\dothack\\Dot Hack Part 2 - Mutation (USA)\\Slus_205.62", // No .debug
    //"C:\\Users\\Admin\\Desktop\\dothack\\Dot Hack Part 3 - Outbreak (USA)\\Slus_205.63", // No .debug
    //"C:\\Users\\Admin\\Desktop\\dothack\\Dot Hack Part 4 - Quarantine (USA)\\Slus_205.64", // No .debug
    "C:\\Users\\Admin\\Desktop\\dothack\\PS2 - .hack vol 1 E3 2002\\Slus_202.67"
};

foreach (var file in files)
{
    Console.WriteLine($"Testing: {file}");
    TestRunner tr = new(file);
    tr.Run();
}

Console.WriteLine("Press enter to exit");
Console.ReadLine();
