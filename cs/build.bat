@echo off

set "SOLUTION_FOLDER=RileySoft.DotHack"
set "SOLUTION_FILE=Rileysoft.DotHack/Rileysoft.DotHack.sln"
set "MSBUILD_PATH=C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\msbuild.exe"
set "VSTEST_PATH=C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe"
set "BIN_DIR=bin"
set "RELEASE_SUFFIX=Release"
set "OUTPUT_DIR=Build"
set "TEST_DLL_PATTERN=bin/**/**/*Tests.dll"
set "TEST_RESULTS_DIR=TestResults"
set "TEST_RESULTS_FILE=*.trx"

for /f "delims=" %%d in ('dir /ad /b /s %SOLUTION_FOLDER%\* ^| findstr /i /e "\bin\Release" ^| findstr /v /i /r /c:".*Tests"') do (
	rd /s /q "%%d"
)

echo Building solution %SOLUTION_FILE%...
"%MSBUILD_PATH%" "%SOLUTION_FILE%" /p:Configuration=Release /t:rebuild
if %ERRORLEVEL% neq 0 (
    pause
)

echo Running tests...
for /f "delims=" %%d in ('dir /ad /b /s %SOLUTION_FOLDER%\* ^| findstr /i /e "\bin\Release" ^| findstr /i /r /c:".*Tests"') do (
  for /f "tokens=*" %%f in ('dir /b %%d\net6.0 ^| findstr /i /r /c:"\.Tests.dll"') do (
	echo "Testing %%d\net6.0\%%f"
	rd /s /q %TEST_RESULTS_DIR%
	"%VSTEST_PATH%" "%%d\net6.0\%%f" /Logger:trx /ResultsDirectory:%TEST_RESULTS_DIR%
	findstr /R /C:"Failed" "%TEST_RESULTS_DIR%\%TEST_RESULTS_FILE%" * >nul && pause
  )  
)

if exist %OUTPUT_DIR% rd /s /q %OUTPUT_DIR%
if not exist %OUTPUT_DIR% mkdir %OUTPUT_DIR%

echo Copying files to %OUTPUT_DIR%...
for /f "delims=" %%d in ('dir /ad /b /s %SOLUTION_FOLDER%\* ^| findstr /i /e "\bin\Release" ^| findstr /v /i /r /c:".*Tests"') do (
  xcopy /e /s /y /q "%%d" "%OUTPUT_DIR%\"
)

for /f "delims=" %%f in ('dir /b "Build\*.nupkg"') do set "filename=%%f" & goto :done
:done

for /f "tokens=2-4 delims=/ " %%a in ('date /t') do set "datestamp=%%c%%a%%b"
set zipfilename=Build\%filename:~0,-6%-full.zip
if exist %zipfilename% del /q %zipfilename%
powershell Compress-Archive -Path "%OUTPUT_DIR%\net6.0\*" -DestinationPath "%zipfilename%"
rd /s /q %OUTPUT_DIR%\net6.0

echo Created a full build with utilities -^> %zipfilename%
echo Nuget package is located -^> %filename%
