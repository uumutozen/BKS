@echo off
setlocal
cd /d "%~dp0"
where dotnet >nul 2>&1
if errorlevel 1 (
  echo .NET SDK bulunamadi.
  exit /b 1
)
dotnet run --project "PdfChecks\BKS.PdfChecks.csproj" -c Release -p:BksOfflineDependencies="%~dp0Dependencies" -- "%~dp0Pdf_Sonuclari"
if errorlevel 1 exit /b 1
exit /b 0
