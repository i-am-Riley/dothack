using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rileysoft.DotHack.DWARF
{
    public enum DW_AT
    {
        Unknown = 0x0000,
        AT_sibling = 0x0012,
        AT_name = 0x0038,
        AT_stmt_list = 0x0106,
        AT_low_pc = 0x0110,
        AT_high_pc = 0x0121,
        AT_language = 0x0136,
        AT_producer = 0x0258
    }
}
