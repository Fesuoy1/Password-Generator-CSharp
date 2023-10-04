@echo off
dotnet build PasswordGenerator.csproj -c Release

if errorLevel 1 (
    echo Build Failed
    )
pause