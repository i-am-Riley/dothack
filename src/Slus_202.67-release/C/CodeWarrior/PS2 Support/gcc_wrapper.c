/**
	MW MIPS C Compiler
	C:\CodeWarrior\PS2 Support\gcc_wrapper.c
*/


#include "gcc_wrapper.h"

/*

addiu sp,-0x10        # Subtract 16 from the stack pointer (allocate 16 bytes for local variables or stack frame)
blez a0, 0x001000EC   # Branch to address 0x001000EC if the value in register a0 is less than or equal to zero
sd ra, (sp)           # Store the return address (ra) onto the stack
jal __floatdisf       # Jump and link to the __floatdisf function
nop                   # No operation (instruction slot reserved for future use)
b 0x00100104          # Unconditional branch to address 0x00100104
ld ra, (sp)           # Load the return address (ra) from the stack
dsrl v1, a0, 0x01     # Shift logical right by 1 bit: v1 = a0 / 2
andi v0, a0, 0x0001   # Bitwise AND with 0x0001: v0 = a0 & 0x0001 (extract the least significant bit)
jal __floatdisf       # Jump and link to the __floatdisf function
or a0, v1, v0         # Bitwise OR: a0 = v1 | v0 (combine v1 and v0)
add.s f00, f00, f00   # Floating-point add: f00 = f00 + f00 (add f00 to itself)
ld ra, (sp)           # Load the return address (ra) from the stack
jr ra                 # Jump to the return address (ra) to return from the function
addiu sp, 0x10        # Add 16 to the stack pointer (deallocate the stack frame)

*/

float _f_ulltof(unsigned long long ul)
{
	if (ul <= 0)
	{
		return 0.0f;
	}
	
	unsigned long long v1 = ul >> 1;
	unsigned long long v0 = ul & 1;
	float f00 = __floatdisf(v1 | v0);
	return f00 + f00;
}
