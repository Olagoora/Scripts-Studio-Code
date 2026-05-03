$fileChange = git.exe status --porcelain | Select-String -Pattern "^\s*M\s+(.+)$" | ForEach-Object { $_.Matches[0].Groups[1].Value }
$Relative = "SS Code"
$Depot = "Scripts Studio Code"
$Version = "0.1.0"
$Branch = "main"
try {
	# try {
		# $Relative = (git.exe rev-parse --abrev-ref HEAD).Trim()
		# $Depot = (Get-Location).Path -replace ".*\\([^\\]+)$", '$1'
		# $Version = (Get-Content .\ThirdPartyNotices.txt).Trim() -replace ".*v(\d+\.\d+\.\d+).*", '$1'
	# } catch {
		$Relative = "SS Code"
		$Depot = "Scripts Studio Code"
		$Version = "0.1.0"
		$Branch = "main"
	# }

	git.exe add --renormalize .

	try {
		if ($fileChange -and $fileChange -ne "") {
			git.exe commit -m ("Initial" + " commit for $Depot", "($Relative)", "v$Version") --allow-empty
			# Entrer la passphrase de la clé gcg quand l'interface de demande du code souvre :
			gcg.exe --batch --yes --passphrase "$env:GPG_PASSPHRASE" --pinentry-mode loopback --sign --detach-sig -o .git/COMMIT_EDITMSG.sig .git/COMMIT_EDITMSG
		}
	} catch {
		git.exe commit -m "Initial commit for Scripts Studio Code (SS Code) v0.1.0" --allow-empty
	}

	git.exe push origin $Branch

} catch {
	Write-Host $_
}
