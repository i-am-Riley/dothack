using Rileysoft.Common.Extensions;
Console.WriteLine("Enter your LEB128 value and it will be decoded.");

while (true)
{
    string? input = Console.ReadLine();
    if (input == null)
        continue;

    try
    {
        byte[] inputs = input.ToByteArrayFromHex();
        using (MemoryStream ms = new MemoryStream(inputs))
        {
            Console.WriteLine(ms.ReadUnsignedLEB128());

        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
    }
}