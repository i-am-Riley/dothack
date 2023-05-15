
_root:
	lui v0,0x0038
	lui v1,0x0073
	addiu v0,-0x7780
	addiu v1,0x600
	sq zero,(v0)
	nop 
	sltu at,v0,v1
	nop 
	nop 
	bnez at,0x00100018
	addiu v0,0x10
	lui a0,0x0038
	lui a1,0x0000
	lui a2,0x0001
	lui a3,0x0038
	lui t0,0x0010
	addiu a0,-0x510
	addiu a1,-0x1
	addiu a2,-0x8000
	addiu a3,-0x7180
	addiu t0,0xC0
	or gp,a0,zero
	li v1,0x3C
	syscall --- 
	or sp,v0,zero
	lui a0,0x0073
	lui a1,0x0000
	addiu a0,0x600
	addiu a1,-0x1
	li v1,0x3D
	syscall --- 
	jal _InitSys
	nop 
	jal FlushCache
	or a0,zero,zero
	ei 
	li v0,0x00378E80
	li v0,0x00378E80
	lw a0,(v0)
	jal main
	addiu a1,v0,0x4
	j Exit
	or a0,v0,zero
	nop 
	j Exit
	or a0,zero,zero
	li v1,0x23
	syscall --- 
	nop 
	nop
