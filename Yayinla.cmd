@echo off
call "%~dp0Derle_ve_Yayinla.cmd"
set "bks_build_result=%errorlevel%"
pause
exit /b %bks_build_result%
