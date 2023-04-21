# Cheat engine searching in pcsx2-qt
There's likely a better way to do this, but this is what I know works for this moment:  

In settings -> scan settings, ensure ``MEM_MAPPED`` is enabled. Restart CE if this has been changed.  
In .hack//infection, in an unmodded copy, search for Kite in the PCSX2 debugger and in CE.
First address for Kite is likely 37FC10 in EE's memory if you're on the The World screen.
in CE you should be looking for a Kite with a /BASLUS-20267DOTHACK string underneath.

If that is it, add that address and subtract the EE memory address and confirm if the address is still readable (not ??)
If modifying this modifies the debug window output, it is the correct address.

