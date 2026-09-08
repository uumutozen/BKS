@echo off
setlocal
cd /d "%~dp0"
where dotnet >nul 2>&1
if errorlevel 1 (
  echo .NET SDK bulunamadi. Bu kaynak paketi icin .NET 8 veya daha yeni SDK gereklidir.
  exit /b 1
)
dotnet publish "BKS\BKS.csproj" -c Release -r win-x64 --self-contained false -p:BksOfflineDependencies="%~dp0Dependencies" -o "%~dp0Uygulama"
if errorlevel 1 (
  echo Derleme tamamlanamadi. Yukaridaki ilk MSBuild veya C# hatasini kontrol edin.
  exit /b 1
)
exit /b 0
