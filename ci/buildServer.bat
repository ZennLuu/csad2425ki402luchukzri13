@echo off
REM Exit on error
setlocal enabledelayedexpansion

set ESP32ProjectPath=..\server
set Board=esp32:esp32:esp32
set requiredCore=esp32:esp32

REM Check if arduino-cli is installed
where arduino-cli >nul 2>&1
if %errorlevel% neq 0 (
    echo arduino-cli is not installed. Exiting...
    exit /b
)

echo arduino-cli checked.

REM Check for available COM port
for /f "tokens=1,2 delims= " %%i in ('arduino-cli board list') do (
    echo %%i | findstr "COM[0-9]" >nul
    if !errorlevel! equ 0 (
        set comPortNumber=%%i
        echo Found device on port: !comPortNumber!
        goto COM_GOOD
    )
)

echo Connected board not found.
exit /b

:COM_GOOD
REM Check if the required core is installed
for /f "tokens=*" %%i in ('arduino-cli core list') do (
    set output=%%i
    echo !output! | findstr /c:"%requiredCore%" >nul
    if !errorlevel! equ 0 (
	echo %requiredCore% core checked.
        goto BUILD
    )
)

echo Core %requiredCore% is NOT installed. Installing it now...
arduino-cli config init
arduino-cli config set board_manager.additional_urls "https://raw.githubusercontent.com/espressif/arduino-esp32/gh-pages/package_esp32_index.json"
arduino-cli core update-index
arduino-cli core install %requiredCore%

:BUILD
echo Building Server...
cd %ESP32ProjectPath%
arduino-cli compile --fqbn %Board% .

echo Uploading bytecode to the board on port %comPortNumber%...
arduino-cli upload -p %comPortNumber% --fqbn %Board% .

cd ..\ci

echo Server build process completed successfully.
endlocal

PAUSE