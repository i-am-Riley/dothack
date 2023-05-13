// See https://aka.ms/new-console-template for more information
using Rileysoft.DotHack.Extensions;
using Rileysoft.DotHack.FileFormats.ELF;
using Rileysoft.DotHack.Metrowerks.MipsCCompiler;
using System.Linq;

Console.WriteLine("ElfInfo");

while (true)
{
    try
    {
        Console.Write("Path: ");
        string? path = Console.ReadLine();
        if (path == null)
        {
            Console.WriteLine("no value");
            continue;
        }

        ElfData elfData = new ElfData();
        string comment = "";

        using (FileStream fs = File.OpenRead(path))
        {

            elfData.ReadFromStream(fs);
            comment = elfData.GetComment(fs);
        }

        elfData.DbgIdent();
        elfData.DbgEhdr();
        elfData.DbgPhdr();
        elfData.DbgShdr();

        if (comment.Length > 0)
        {
            Console.WriteLine("");
            Console.WriteLine($"Comment: {comment}");
        }

        elfData.DumpStrtbl("strtbl");
        Console.WriteLine("Saved strtbl");
    }
    catch (Exception e)
    {
        Console.WriteLine(e.ToString());
    }
}


