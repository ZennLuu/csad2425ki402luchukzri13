@echo off
set projectPath=..\client\client.csproj

dotnet build %projectPath% -c Release
echo Client build process completed successfully.