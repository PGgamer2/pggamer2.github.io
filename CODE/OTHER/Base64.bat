@echo off
IF [%1] EQU [] GOTO ERROR
:S
set /p dore="Decode or Encode?(D/E) "
set /p name="Name of the output file? "
if %dore%==D GOTO DEC
if %dore%==E GOTO ENC

:DEC
certutil -decode %1 %name%
ECHO.
ECHO Done!
PAUSE
EXIT

:ENC
certutil -encode %1 %name%
ECHO.
ECHO Done!
PAUSE
EXIT

:ERROR
ECHO Drag and drop a file.
PAUSE
EXIT