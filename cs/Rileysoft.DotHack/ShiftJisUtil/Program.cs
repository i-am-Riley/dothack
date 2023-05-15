using System.Text;
using Rileysoft.DotHack.Extensions;
using Rileysoft.DotHack.ShiftJIS;

Console.WriteLine("Start a line with > to convert to shift_jis");
Console.WriteLine("Start a line with < to convert from shift_jis");

while (true)
{
    string? input = Console.ReadLine();
    if (input == null || input.Length < 2)
        continue;

    string after = input.Substring(1);
    try
    {
        if (input.Substring(0, 1) == ">")
        {
            var afterBytes = ShiftJISEncoding.GetBytes(after);
            var asAscii = Encoding.ASCII.GetString(afterBytes);
            Console.WriteLine("As ASCII: " + asAscii);
            Console.WriteLine("Hex: " + afterBytes.ToStringHexExpanded());
        }
        else if (input.Substring(0, 1) == "<")
        {
            var asciiBytes = Encoding.ASCII.GetBytes(after);
            var shiftJisConversion = ShiftJISEncoding.GetString(asciiBytes);
            Console.WriteLine("Converted: " + shiftJisConversion);

        }
        else
        {
            Console.WriteLine("unrecognized start character");
        }
    }
    catch (Exception e)
    {
        Console.WriteLine("err: " + e.ToString());
    }
}
