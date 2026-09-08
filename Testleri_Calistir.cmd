@echo off
setlocal
cd /d "%~dp0"
where dotnet >nul 2>&1
if errorlevel 1 (
  echo .NET SDK bulunamadi.
  exit /b 1
)
dotnet run --project "Tests\BKS.Checks.csproj" -c Release
if errorlevel 1 exit /b 1
exit /b 0
