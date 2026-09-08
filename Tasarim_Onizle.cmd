@echo off
setlocal
if not exist "%~dp0Uygulama\BKS.exe" (
  call "%~dp0Derle_ve_Yayinla.cmd"
  if errorlevel 1 (
    pause
    exit /b 1
  )
)
start "" "%~dp0Uygulama\BKS.exe" --ui-preview
