@echo off
>nul 2>&1 "%SYSTEMROOT%\system32\cacls.exe" "%SYSTEMROOT%\system32\config\system"

if '%errorlevel%' NEQ '0' (
    echo Requesting administrative privileges...
    goto UACPrompt
) else ( goto gotAdmin )

:UACPrompt
    echo Set UAC = CreateObject^("Shell.Application"^) > "%temp%\getadmin.vbs"
    set params = %*:"=""
    echo UAC.ShellExecute "cmd.exe", "/c %~s0 %params%", "", "runas", 1 >> "%temp%\getadmin.vbs"

    "%temp%\getadmin.vbs"
    del "%temp%\getadmin.vbs"
    exit /B

:gotAdmin
    pushd "%CD%"
    CD /D "%~dp0"

@echo off
net session >nul 2>&1
if not %errorlevel% == 0 (
    set /p=Running without administrative privileges. Please rerun as admin.
    exit /b
)

set "script_dir=%~dp0"
set "pause_flag="

@echo off
:run_setup
setlocal enabledelayedexpansion
set "dotnet5="
set "dotnet8="
for /f "tokens=*" %%i in ('dotnet --list-sdks') do (
    set "sdk=%%i"
    echo !sdk! | findstr "5.0" >nul
    if !errorlevel! equ 0 (
        set "dotnet5=installed"
    )
    echo !sdk! | findstr "8.0" >nul
    if !errorlevel! equ 0 (
        set "dotnet8=installed"
    )
)

if defined dotnet5 (
    echo .NET 5.0 is already installed.
) else (
    if not exist cache/x86-5.0.exe (
        mkdir cache
        echo Downloading .NET 5.0 x64 into ./cache/x64-5.0.exe
        curl -L -o cache/x64-5.0.exe https://download.visualstudio.microsoft.com/download/pr/14ccbee3-e812-4068-af47-1631444310d1/3b8da657b99d28f1ae754294c9a8f426/dotnet-sdk-5.0.408-win-x64.exe
        echo Downloading .NET 5.0 x86 into ./cache/x86-5.0.exe
        curl -L -o cache/x86-5.0.exe https://download.visualstudio.microsoft.com/download/pr/d20a2521-d273-4ce3-b740-f9b2c363d110/e569a7b31d816d2f04baa81bf06a59ba/dotnet-sdk-5.0.408-win-x86.exe
    )
    echo Running .NET 5.0 x64 Installation...
    start /W cache/x64-5.0.exe
    echo Running .NET 5.0 x86 Installation...
    start /W cache/x86-5.0.exe
)

if defined dotnet8 (
    echo .NET 8.0 is already installed.
) else (
    if not exist cache/x86-8.0.exe (
        if not exist cache (
            mkdir cache
        )
        echo Downloading .NET 8.0 x64 into ./cache/x64-8.0.exe
        curl -L -o cache/x64-8.0.exe https://download.visualstudio.microsoft.com/download/pr/90486d8a-fb5a-41be-bfe4-ad292c06153f/6673965085e00f5b305bbaa0b931cc96/dotnet-sdk-8.0.300-win-x64.exe
        echo Downloading .NET 8.0 x86 into ./cache/x86-8.0.exe
        curl -L -o cache/x86-8.0.exe https://download.visualstudio.microsoft.com/download/pr/9736c2dc-c21d-4df6-8cb7-9365ed5461a9/4c360dc61c7cb6d26b48d2718341c68e/dotnet-sdk-8.0.300-win-x86.exe
    )
    echo Running .NET 8.0 x64 Installation...
    start /W cache/x64-8.0.exe
    echo Running .NET 8.0 x86 Installation...
    start /W cache/x86-8.0.exe
)

endlocal


if "%1"=="--pause" (
    set "pause_flag=1"
)

if defined pause_flag (
    pause
)

set /p run_install=Do you want to run the install.bat inside the CORE directory? [Y/N] 
if /i "%run_install%"=="Y" (
    powershell -Command "Start-Process '%script_dir%CORE\install.bat' -Verb RunAs"
    set /p=Select Install and press ENTER once complete to continue...
) else (
    echo Skipping install.bat execution.
)

@echo off
set /p copy_files=Do you want to copy MOSA-Core files (required for working build)? [Y/N] 
if /i "%copy_files%"=="Y" (
    echo Copying MOSA-Core...
    mkdir "C:\Program Files\MOSA-Core"
    xcopy "%script_dir%\CORE\src\*.*" "%programfiles%\MOSA-Core" /S /Q
    pause
) else (
    echo Skipping CORE copy...
)

if defined pause_flag (
    pause
)
