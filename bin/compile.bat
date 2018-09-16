@echo off
call "C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\Tools\VsDevCmd.bat"
cl %1 /nologo /TC /Za /Fe: %2 /Fo: %3 >%4 2>&1
