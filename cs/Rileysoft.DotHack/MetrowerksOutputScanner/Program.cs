// See https://aka.ms/new-console-template for more information
Console.WriteLine("Metrowerks Output Scanner");

Console.WriteLine("Input a path to an ELF file compiled with Metrowerks.");
Console.Write("> ");

string? elfFile = Console.ReadLine();
if (elfFile == null)
    return;

if (!File.Exists(elfFile))
{
    Console.WriteLine("File does not exist.");
    return;
}

// see get_debug_symbols.bat
Console.WriteLine("Input a path to the debug output from Metrowerks.");
Console.Write("> ");

string? debugOutputFile = Console.ReadLine();
if (debugOutputFile == null) 
    return;

if (!File.Exists(debugOutputFile))
{
    Console.WriteLine("File does not exist.");
    return;
}

