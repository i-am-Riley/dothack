using Rileysoft.DotHack.FileFormats.ELF;
using Rileysoft.DotHack.Metrowerks.MipsCCompiler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElfDebugTest
{
    public class TestRunner
    {
        private readonly string Path;

        private static void SetColor (string color)
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
                    ElfData elfData = new ElfData(stream);
                    ElfDebug elfDebug = new ElfDebug(elfData, stream);
                    if (!elfDebug.Valid)
                        Console.WriteLine("Not valid for debug");
                }
            }
            catch (Exception e)
            {
                SetColor("41;37");
                Console.Write("Exception!");
                SetColor("0");
                Console.Write("\n");
                Console.WriteLine(e.ToString());

                Console.WriteLine("");
                Console.WriteLine("Press enter to continue...");
                Console.ReadLine();
            }
        }
    }
}
