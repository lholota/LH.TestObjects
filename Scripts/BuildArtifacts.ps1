param(
	[string]$Configuration,
	[string]$Version
)

Write-Host "Building the NuGet package..."
& "Nuget.exe" pack -Verbosity Detailed -Prop Configuration=$Configuration

Write-Host "Creating the website artifacts..."
Create-Directory "_Website\v$Version"

Copy-Item "License" "_Website\v$Version\License.txt"
Copy-Item "Logo_128.png" "_Website\v$Version\nuget-icon.png"