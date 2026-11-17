$ErrorActionPreference = 'Stop'
$project = Join-Path $PSScriptRoot 'src/CodexStatusbar/CodexStatusbar.csproj'
$dist = Join-Path $PSScriptRoot 'dist'
dotnet publish $project -c Release -r win-x64 --self-contained true -o $dist --nologo
if ($LASTEXITCODE -ne 0) { throw 'Compilation échouée.' }
$exe = Join-Path $dist 'CodexStatusbar.exe'
& $exe --self-test (Join-Path $PSScriptRoot 'fixtures')
if ($LASTEXITCODE -ne 0) { throw 'Autotests échoués.' }
& $exe --wave-preview (Join-Path $dist 'preview')
if ($LASTEXITCODE -ne 0) { throw 'Vérification de la vague échouée.' }
Write-Host "Compilation et contrôles terminés : $exe"
