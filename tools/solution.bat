@echo off

where msbuild >nul 2>&1

if %ERRORLEVEL% EQU 0 GOTO vsdevcmd_executed

REM locate VS2019
for /f "usebackq tokens=*" %%i in (`"%ProgramFiles(x86)%\Microsoft Visual Studio\Installer\vswhere.exe" -version 16.0 -find **\VsDevCmd.bat`) DO (
  call "%%i"
)

:vsdevcmd_executed

pushd %~dp0\..

dotnet slngen -h >nul 2>&1

if %ERRORLEVEL% EQU 0 goto dotnet_tool_restore_executed

dotnet tool restore

:dotnet_tool_restore_executed

REM slngen works only with working msbuild
dotnet slngen --folders:false --nologo --solutionfile:pdfium.sln dirs.proj 

popd