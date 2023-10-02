@echo off
dotnet build

if errorlevel 1 (
    echo Build failed
)
pause
