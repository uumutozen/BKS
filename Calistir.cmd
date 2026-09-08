@echo off
setlocal
cd /d "%~dp0"
if not exist "%~dp0Uygulama\BKS.exe" (
  echo Ilk acilis: uygulama kaynak koddan derleniyor...
  call "%~dp0Derle_ve_Yayinla.cmd"
  if errorlevel 1 (
    pause
    exit /b 1
  )
)
start "" "%~dp0Uygulama\BKS.exe"
