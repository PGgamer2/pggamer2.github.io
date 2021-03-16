@echo off
TITLE Youtube Downloader

:INITC
Ping www.google.com -n 1 -w 1000 >nul
CLS
if errorlevel 1 (GOTO NOCONN)

if not exist "%~dp0youtube-dl.exe" GOTO YTDL
if not exist "%~dp0ffmpeg.exe" GOTO FFMPEG
GOTO ST

:YTDL
ECHO A file/s is missing...
if not exist "%~dp0youtube-dl.exe" powershell -Command "Invoke-WebRequest https://youtube-dl.org/downloads/latest/youtube-dl.exe -OutFile youtube-dl.exe"
CLS
IF %ERRORLEVEL% NEQ 0 (ECHO Sorry, an error has occurred) ELSE (ECHO File successfully downloaded)
if not exist "%~dp0ffmpeg.exe" (GOTO FFMPEG) ELSE (GOTO ST)

:FFMPEG
ECHO A file/s is missing...
if not exist "%~dp0ffmpeg.exe" powershell -Command "Invoke-WebRequest https://ffmpeg.zeranoe.com/builds/win32/static/ffmpeg-latest-win32-static.zip -OutFile ffmpeg-temp.zip"
Call :UnZipFile "%~dp0" "%~dp0ffmpeg-temp.zip"

:UnZipFile
set vbs="%temp%\_.vbs"
if exist %vbs% del /f /q %vbs%
>%vbs%  echo Set fso = CreateObject("Scripting.FileSystemObject")
>>%vbs% echo If NOT fso.FolderExists(%1) Then
>>%vbs% echo fso.CreateFolder(%1)
>>%vbs% echo End If
>>%vbs% echo set objShell = CreateObject("Shell.Application")
>>%vbs% echo set FilesInZip=objShell.NameSpace(%2).items
>>%vbs% echo objShell.NameSpace(%1).CopyHere(FilesInZip)
>>%vbs% echo Set fso = Nothing
>>%vbs% echo Set objShell = Nothing
cscript //nologo %vbs%
if exist %vbs% del /f /q %vbs%

move "%~dp0ffmpeg-latest-win32-static\bin\ffmpeg.exe" "%~dp0" >nul
del "%~dp0ffmpeg-temp.zip" >nul
RD /S /Q "%~dp0ffmpeg-latest-win32-static" >nul
CLS
IF %ERRORLEVEL% NEQ 0 (ECHO Sorry, an error has occurred) ELSE (ECHO File/s successfully downloaded)
if not exist "%~dp0youtube-dl.exe" (GOTO YTDL) ELSE (GOTO ST)

:ST
set dwf="%~dp0Download"
if not exist %dwf% mkdir %dwf% >nul
for /R %%x in (*.mp4) do move "%%x" %dwf% >nul
for /R %%x in (*.mp3) do move "%%x" %dwf% >nul

set /P url="URL? "
set /P voa="Video or Audio?(V/A) "
for /f "usebackq delims=" %%I in (`powershell "\"%voa%\".toUpper()"`) do set "voa=%%~I"
if %voa%==V GOTO VIDEO
if %voa%==A GOTO AUDIO
CLS
ECHO Sorry, an error has occurred
GOTO ST

:VIDEO
CLS
youtube-dl -f mp4 %url%
if not exist %dwf% mkdir %dwf%
for /R %%x in (*.mp4) do move "%%x" %dwf% >nul
CLS
IF %ERRORLEVEL% NEQ 0 (ECHO Sorry, an error has occurred) ELSE (ECHO File/s successfully downloaded)
GOTO ST

:AUDIO
CLS
youtube-dl -x --audio-format mp3 %url%
if not exist %dwf% mkdir %dwf%
for /R %%x in (*.mp3) do move "%%x" %dwf% >nul
CLS
IF %ERRORLEVEL% NEQ 0 (ECHO Sorry, an error has occurred) ELSE (ECHO File/s successfully downloaded)
GOTO ST

:NOCONN
ECHO The device isn't connected to internet...
timeout /T 5
GOTO INITC