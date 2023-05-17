using Rileysoft.DotHack.Metrowerks.CATS;
using Rileysoft.FileFormats.ELF;

namespace ElfDebugTest
{
    public class TestRunner
    {
        private readonly string Path;

        private static void SetColor(string color)
        {
            Console.Write($"\u001b[{color}m");
        }

        public TestRunner(string path)
        {
            Path = path;
        }

        public void Run()
        {
            try
            {
                using (FileStream stream = File.OpenRead(Path))
                {
                    ElfData elfData = new(stream);

                    try
                    {
                        var mwcatsSections = CATSInfo.ReadAllFromStream(stream, elfData);
                        foreach (var section in mwcatsSections)
                        {
                            Console.WriteLine($".mwcats section has {section.Sections.Count} CATSInfoSections");
                        }

                        return;
                    }
                    catch (Exception e)
                    {
                        SetColor("41;37");
                        Console.Write(e.Message);
                        SetColor("0");
                        Console.Write("\n");

                        Console.WriteLine("");
                    }

                }
            }
            catch (Exception e)
            {
                SetColor("41;37");
                Console.Write(e.Message);
                SetColor("0");
                Console.Write("\n");

                Console.WriteLine("");
            }
        }
    }
}
