$scriptPath = Split-Path $PSCommandPath
Write-Host "Script is running on root: "$scriptPath

$version = "4.7.0"

# Install psake
.\tools\NuGet.exe install psake -version $version

if((Get-Module psake) -eq $null) {
    "Importing psake"
    Import-Module (Join-Path $scriptPath psake.$version/tools/psake/psake.psm1)
}

Invoke-psake (Join-Path $scriptPath ./psakefile.ps1) -parameters @{
    "build_configuration"="Debug"
}