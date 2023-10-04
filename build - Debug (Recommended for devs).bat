@echo off
dotnet build PasswordGenerator.csproj -c Debug

if errorLevel 1 (
    echo Build Failed
    )
pause