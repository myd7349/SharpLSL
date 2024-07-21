@echo off
powershell.exe -NoProfile -ExecutionPolicy Bypass "& {& '%~dp0generate.ps1' %*}"

:: References:
:: https://github.com/microsoft/vcpkg/blob/master/bootstrap-vcpkg.bat
