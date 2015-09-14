param(
	[string]$Configuration,
	[string]$Version
)

Write-Host "Configuration: $Configuration"
Write-Host "Version: $Version"

$ErrorActionPreference = "Stop"

Write-Host "Building the NuGet package..."
Start-Process -FilePath "$PSScriptRoot\Nuget.exe" -WorkingDirectory "..\LH.TestObjects\" -Wait -NoNewWindow -ArgumentList "pack -Verbosity Detailed -Prop Configuration=$Configuration"
# & "$PSScriptRoot\Nuget.exe" pack -Verbosity Detailed -Prop Configuration=$Configuration

Write-Host "Creating the website artifacts..."
New-Item "..\_Artifacts\v$Version" -ItemType Directory

Copy-Item "..\License" "..\_Artifacts\v$Version\license.txt"
Copy-Item "..\Logo_128.png" "..\_Artifacts\v$Version\nuget-icon.png"
Copy-Item "..\LH.TestObjects\*.nupkg" "..\_Artifacts\"