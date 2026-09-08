@echo off
setlocal
if not exist "%~dp0Uygulama\BKS.exe" (
  call "%~dp0Derle_ve_Yayinla.cmd"
  if errorlevel 1 exit /b 1
)
start "" /wait "%~dp0Uygulama\BKS.exe" --layout-checks "%~dp0Yerlesim_Sonuclari"
set "bks_check_result=%errorlevel%"
type "%~dp0Yerlesim_Sonuclari\Windows_Yerlesim_Sonucu.txt"
pause
exit /b %bks_check_result%
