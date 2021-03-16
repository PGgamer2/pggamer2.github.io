@echo off
CLS
ECHO.
ECHO.
powershell write-host -fore DarkRed WIN 10 Secrets
ECHO.
@timeout /t 3
:START
CLS
ECHO.
ECHO 1.Slide to Shutdown
ECHO 2.Secret File Explorer
ECHO 3.God Mode
ECHO 4.Toggle Crash Code on BSOD  (Requires Administrator Permission)
ECHO.
set /p choice="Enter your choice:"
IF %choice%==4 GOTO IBSOD
IF %choice%==3 GOTO GodMode
IF %choice%==2 GOTO SFE
IF %choice%==1 GOTO STSH
GOTO END
:STSH
powershell "$s=(New-Object -COM WScript.Shell).CreateShortcut('%userprofile%\Desktop\Slide to Shutdown.lnk');$s.IconLocation='%SystemRoot%\System32\SHELL32.dll, 27';$s.TargetPath='%SystemRoot%\System32\SlideToShutDown.exe';$s.Save()"
GOTO END
:SFE
powershell "$s=(New-Object -COM WScript.Shell).CreateShortcut('%userprofile%\Desktop\Secret File Explorer.lnk');$s.IconLocation='%SystemRoot%\explorer.exe, 0';$s.TargetPath='%SystemRoot%\explorer.exe';$s.Arguments='shell:AppsFolder\c5e2524a-ea46-4f67-841f-6a9465d9d515_cw5n1h2txyewy!App';$s.Save()"
GOTO END
:GodMode
mkdir "%userprofile%\Desktop\GodMode.{ED7BA470-8E54-465E-825C-99712043E01C}"
GOTO END
:IBSOD
ECHO.
set /p CONFREG="Do you want to add or to remove this function?(A/R)"
IF %CONFREG%==A REG ADD HKLM\SYSTEM\CurrentControlSet\Control\CrashControl /v DisplayParameters /t REG_DWORD /d 1
IF %CONFREG%==R REG DELETE HKLM\SYSTEM\CurrentControlSet\Control\CrashControl /v DisplayParameters
GOTO END
:END
ECHO.
PAUSE
GOTO START