@echo off
for /f "tokens=*" %%i in ('dir/s/b/ad^|sort /r') do (
	dir/a/b "%%i\"|findstr . >nul&&( echo ����:"%%i" )||( 
														echo ��:"%%i"
														echo %date%%time%>"%%i"\nil 
														)
)
pause