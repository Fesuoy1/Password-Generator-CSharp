@echo off

cd "C:\Users\Fesuoy\source\repos\password generator\password generrator C#"
dotnet build

if errorlevel 1 (
    echo Build failed
)
pause