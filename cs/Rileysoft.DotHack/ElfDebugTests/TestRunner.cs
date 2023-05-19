using Rileysoft.DotHack.Metrowerks.CATS;
using Rileysoft.FileFormats.ELF;
using System.Diagnostics;

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
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                using (FileStream stream = File.OpenRead(Path))
                {
                    ElfData elfData = new(stream);

                    try
                    {
                        long memoryUsageBefore = GC.GetTotalMemory(false);
                        var mwcatsSections = CATSInfo.ReadAllFromStream(stream, elfData);
                        foreach (var section in mwcatsSections)
                        {
                            Console.WriteLine($".mwcats section has {section.Sections.Count} CATSInfoSections");
                        }
                        Console.WriteLine("Parsed in " + stopwatch.Elapsed.ToString());
                        stopwatch.Stop();
                        long memoryUsageAfter = GC.GetTotalMemory(false);

                        long difference = memoryUsageAfter - memoryUsageBefore;
                        Console.WriteLine("Memory Usage Change: " + difference);
                        Console.WriteLine("");

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
            Console.WriteLine("Parsed in " + stopwatch.Elapsed.ToString());
            stopwatch.Stop();
        }

    }
}
