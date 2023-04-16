# SYSTEM.CNF
This file is top-level and defines a few parameters for the PS2 to use to run the program.

## Contents
These are the contents for the copy of .hack//infection's ``System.cnf``
```
BOOT2 = cdrom0:\SLUS_202.67;1
VER = 1.00
VMODE = NTSC

```

A ``PARAM2`` and ``PARAM4`` can also be present but aren't.

## Loading in C#
For C# reference the following for use to load a CNF file:  
[CnfFile](https://github.com/i-am-Riley/dothack/blob/main/cs/Rileysoft.DotHack/Rileysoft.DotHack/FileFormats/CNF/CnfFile.cs) - Performs I/O to read/write CNF data.  
[CnfData](https://github.com/i-am-Riley/dothack/blob/main/cs/Rileysoft.DotHack/Rileysoft.DotHack/FileFormats/CNF/CnfData.cs) - Defines CNF data.  

Code Example:
```cs
using Rileysoft.DotHack.FileFormats.CNF;

var file = new CnfFile("SYSTEM.CNF", true); // Reads SYSTEM.CNF and sets the underlying data to readonly.
var cnfData = file.Data;
```

## References
- Reference [this constructor](https://github.com/i-am-Riley/dothack/blob/2e1323c5b0c44c8092cf7bfc0e69eeb2741f4ecf/cs/Rileysoft.DotHack/Rileysoft.DotHack/FileFormats/CNF/CnfFile.cs#L32) for more information on the constructor used in this example.
- Reference [PS Dev Wiki](https://www.psdevwiki.com/ps2/System.cnf) for more information on ``System.cnf``.
