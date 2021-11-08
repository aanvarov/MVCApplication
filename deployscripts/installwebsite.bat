%SystemRoot%\sysnative\WindowsPowerShell\v1.0\powershell.exe -command "Set-ExecutionPolicy Unrestricted -Force"
IF EXIST C:\webApps\MVCWebApplication rmdir C:\webApps\MVCWebApplication
mkdir C:\webApps\MVCWebApplication

cd c:\temp

%SystemRoot%\sysnative\WindowsPowerShell\v1.0\powershell.exe -command ".\installwebsite.ps1"