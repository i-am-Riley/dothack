// See https://aka.ms/new-console-template for more information
using ElfDebugTest;

Console.WriteLine("ELF debug test...");

var files = new string[]
{
    "C:\\Users\\Admin\\Desktop\\dothack\\Dot Hack Kansen Kakudai Vol. 1 (Japan)\\Slps_251.21",
    "C:\\Users\\Admin\\Desktop\\dothack\\Dot Hack Part 1 - Infection (USA) (Demo 2)\\bandai.elf",
    "C:\\Users\\Admin\\Desktop\\dothack\\Dot Hack Part 1 - Infection (USA) (En,Ja)\\Slus_202.67",
    "C:\\Users\\Admin\\Desktop\\dothack\\Dot Hack Part 2 - Mutation (USA)\\Slus_205.62",
    "C:\\Users\\Admin\\Desktop\\dothack\\Dot Hack Part 3 - Outbreak (USA)\\Slus_205.63",
    "C:\\Users\\Admin\\Desktop\\dothack\\Dot Hack Part 4 - Quarantine (USA)\\Slus_205.64",
    "C:\\Users\\Admin\\Desktop\\dothack\\PS2 - .hack vol 1 E3 2002\\Slus_202.67"
};

foreach (var file in files)
{
    Console.WriteLine($"testing {file}");
    TestRunner tr = new TestRunner(file);
    tr.Run();
}
