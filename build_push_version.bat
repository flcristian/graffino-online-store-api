@echo off
call service_version_number.bat

if not defined SERVICE_VERSION (
    echo Error: SERVICE_VERSION is not defined in service_version_number.bat.
    exit /b 1
)

echo Building Docker image...
docker build -f Dockerfile -t graffino-online-store-api:%SERVICE_VERSION% .

echo Getting the new image ID...
setlocal enabledelayedexpansion
for /f "tokens=*" %%i in ('docker images -q graffino-online-store-api:%SERVICE_VERSION%') do set IMAGE_ID=%%i
echo New image ID: !IMAGE_ID!
endlocal

echo Tagging the image...
docker tag graffino-online-store-api:%SERVICE_VERSION% florescucristian/graffino-online-store-api:%SERVICE_VERSION%

echo Pushing the tagged image...
docker push florescucristian/graffino-online-store-api:%SERVICE_VERSION%

echo Script completed.

pause