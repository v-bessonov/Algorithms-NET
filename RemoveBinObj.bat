for /d /r "%1" %%d in (bin) do @if exist "%%d" rd /s/q "%%d"
for /d /r "%1" %%d in (obj) do @if exist "%%d" rd /s/q "%%d"
pause