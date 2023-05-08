@echo off

set "SOLUTION_FILE=Rileysoft.DotHack/Rileysoft.DotHack.sln"
set "MSBUILD_PATH=C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\msbuild.exe"
set "VSTEST_PATH=C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe"
set "TEST_DLL_PATTERN=bin\**\*.Tests.dll"
set "BIN_DIR=bin"
set "RELEASE_SUFFIX=Release"
set "OUTPUT_DIR=Build"

for /f "delims=" %%d in ('dir /ad /b /s Rileysoft.DotHack\* ^| findstr /i /e "\bin\Release" ^| findstr /v /i /r /c:".*Tests"') do (
	rd /s /q "%%d"
)

echo Building solution %SOLUTION_FILE%...
"%MSBUILD_PATH%" "%SOLUTION_FILE%" /p:Configuration=Release
if %ERRORLEVEL% neq 0 (
    pause
)

echo Running tests...
for /r %%i in (%TEST_DLL_PATTERN%) do (
  "%VSTEST_PATH%" "%%i"
  if %ERRORLEVEL% neq 0 (
    echo "There are failed tests."
    pause
  )
)

if exist %OUTPUT_DIR% rd /s /q %OUTPUT_DIR%
if not exist %OUTPUT_DIR% mkdir %OUTPUT_DIR%

echo Copying files to %OUTPUT_DIR%...
for /f "delims=" %%d in ('dir /ad /b /s Rileysoft.DotHack\* ^| findstr /i /e "\bin\Release" ^| findstr /v /i /r /c:".*Tests"') do (
  xcopy /e /s /y "%%d" "%OUTPUT_DIR%\"
)

for /f "tokens=2-4 delims=/ " %%a in ('date /t') do set "datestamp=%%c%%a%%b"
set zipfilename=Build\Rileysoft_DotHack_%datestamp%.zip
if exist %zipfilename% del /q %zipfilename%
powershell Compress-Archive -Path "%OUTPUT_DIR%\net6.0" -DestinationPath "%zipfilename%"

echo Done.