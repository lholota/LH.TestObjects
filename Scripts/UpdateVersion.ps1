param(
	[string]$Version,
	[string]$FilePath
)

Function Update-AssemblyInfo($FilePath, $Version){
	$Content = Get-Content -Path $FilePath -Encoding UTF8 -Raw
	
	$Content = $Content.Replace("[assembly: AssemblyVersion(`"1.0.0.0`")]", "[assembly: AssemblyVersion(`"$Version`")]")
	$Content = $Content.Replace("[assembly: AssemblyFileVersion(`"1.0.0.0`")]", "[assembly: AssemblyFileVersion(`"$Version`")]")

	Set-Content -Path $FilePath -Value $Content -Encoding UTF8
}

Update-AssemblyInfo -FilePath $FilePath -Version $Version