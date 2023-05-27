@ECHO OFF
:: from 2020-07-19 05:20:04pm
:: credit to root670 from https://zenius-i-vanisher.com/v5.2/thread?postid=438694#p436618
:: https://bin.jvnv.net/file/T5okj/DDR%20Debug%20Symbols.zip.
SETLOCAL EnableDelayedExpansion

SET CODEWARRIOR_LINKER="C:\Program Files (x86)\Metrowerks\CodeWarrior2\PS2_Tools\Command_Line_Tools\mwldps2.exe"

ECHO Extracting debug symbols...

FOR /R . %%G IN (*.elf) DO (
  echo %%G
  %CODEWARRIOR_LINKER% -dis -show only,debug "%%G" > %%G.txt
  IF !ERRORLEVEL! NEQ 0 (
    ECHO Failed to extract debug symbols.
    EXIT /B 1
  )
)

ENDLOCAL