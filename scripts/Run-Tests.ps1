param(
  [ValidateSet('Debug','Release')]
  [string]$Configuration = 'Debug',

  # Target framework for test run
  [ValidateSet('net8.0','net48')]
  [string]$Framework = 'net8.0',

  # Run only specs with these tags (can be specified multiple times)
  [string[]]$IncludeTags = @(),

  # Exclude specs with these tags (can be specified multiple times)
  [string[]]$ExcludeTags = @(),

  # Shorthand for focusing tagged specs ([Tags("focus")])
  [switch]$Focus,

  # Additional filter expression for dotnet test (overrides tag-derived filter if provided)
  [string]$Filter,

  # Use the legacy MSpec console runner instead of dotnet test
  [switch]$UseConsoleRunner,

  # Skip building before running specs
  [switch]$NoBuild
)

$ErrorActionPreference = 'Stop'
Set-StrictMode -Version Latest

$repoRoot = Resolve-Path (Join-Path $PSScriptRoot '..')
$solution = Join-Path $repoRoot 'Timtek.SemanticQuantities.sln'
$testProj = Join-Path $repoRoot 'SemanticQuantities.Tests\Timtek.SemanticQuantities.Tests.csproj'

if ($UseConsoleRunner) {
  # Console runner path and net48 output
  $testDll  = Join-Path $repoRoot "SemanticQuantities.Tests\bin\$Configuration\net48\Timtek.SemanticQuantities.Tests.dll"
  if (-not $NoBuild) {
    Write-Host "Building $testProj ($Configuration|net48)" -ForegroundColor Cyan
    dotnet build $testProj -c $Configuration -f net48 | Out-Host
  }
  if (-not (Test-Path $testDll)) {
    throw "Test assembly not found: $testDll. Did the build succeed for framework 'net48'?"
  }
  $mspecCandidates = Get-ChildItem "$HOME\.nuget\packages\machine.specifications.runner.console\*\tools\**\mspec*.exe" -File -ErrorAction SilentlyContinue
  if (-not $mspecCandidates) { throw "MSpec console runner not found under $HOME\.nuget\packages. Run 'dotnet restore'." }
  $mspec = $mspecCandidates | Sort-Object LastWriteTime -Descending | Select-Object -First 1
  $args = @($testDll,'--progress')
  if ($Focus) { $args += @('--include','focus') }
  foreach ($t in $IncludeTags) { if ($t) { $args += @('--include', $t) } }
  foreach ($t in $ExcludeTags) { if ($t) { $args += @('--exclude', $t) } }
  Write-Host "Running: $($mspec.FullName) $($args -join ' ')" -ForegroundColor Yellow
  & $mspec.FullName @args
  exit $LASTEXITCODE
}

# dotnet test path
$dotnetArgs = @('test', $solution, '-c', $Configuration, '-f', $Framework, '--no-restore')
if ($NoBuild) { $dotnetArgs += '--no-build' }

# Build filter expression from tags if not explicitly provided
if (-not [string]::IsNullOrWhiteSpace($Filter)) {
  $filterExpr = $Filter
}
else {
  $includeParts = @()
  if ($Focus) { $includeParts += 'TestCategory=focus' }
  foreach ($t in $IncludeTags) { if ($t) { $includeParts += "TestCategory=$t" } }
  $excludeParts = @()
  foreach ($t in $ExcludeTags) { if ($t) { $excludeParts += "TestCategory!=$t" } }
  if ($includeParts.Count -gt 0 -or $excludeParts.Count -gt 0) {
    # Combine includes with OR, then AND with excludes
    $inc = ($includeParts -join '|')
    $exc = ($excludeParts -join '&')
    if ($inc -and $exc) { $filterExpr = "($inc)&$exc" }
    elseif ($inc)      { $filterExpr = $inc }
    else               { $filterExpr = $exc }
  }
}

if ($filterExpr) {
  $dotnetArgs += @('--filter', $filterExpr)
}

Write-Host "Running: dotnet $($dotnetArgs -join ' ')" -ForegroundColor Yellow
$proc = Start-Process -FilePath 'dotnet' -ArgumentList $dotnetArgs -NoNewWindow -PassThru
$proc.WaitForExit()
exit $proc.ExitCode
