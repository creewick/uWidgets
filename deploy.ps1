$solutionDir = ".\"
$releaseDir = Join-Path -Path $solutionDir -ChildPath 'uWidgets\bin\Release\net8.0'
$deployDir = Join-Path -Path $solutionDir -ChildPath 'uWidgets\bin\Deploy'

$runtimes = @('win-x86', 'win-x64', 'win-arm64')

if (Test-Path -Path $releaseDir) {
    Remove-Item -Path $releaseDir -Recurse -Force
}

Write-Output "Building solution"
& dotnet build "$solutionDir\uWidgets.sln" --configuration Release

if (Test-Path -Path $deployDir) {
    Remove-Item -Path $deployDir -Recurse -Force
}
New-Item -Path $deployDir -ItemType Directory | Out-Null

foreach ($runtime in $runtimes) {
    Write-Output "Processing runtime: $runtime"

    $targetDir = Join-Path -Path $deployDir -ChildPath $runtime
    
    if (-not (Test-Path -Path $targetDir)) {
        New-Item -Path $targetDir -ItemType Directory | Out-Null
    }

    Write-Output "Publishing main project for runtime: $runtime"
    & dotnet publish "$solutionDir\uWidgets\uWidgets.csproj" `
        --configuration Release `
        --runtime $runtime `
        --output "$targetDir" `
        --self-contained false

    Write-Output "Copying Widgets for runtime: $runtime"
    $widgetsSrcDir = Join-Path -Path $releaseDir -ChildPath 'Widgets'
    $widgetsDestDir = Join-Path -Path $targetDir -ChildPath 'Widgets'
    if (-not (Test-Path -Path $widgetsDestDir)) {
        New-Item -Path $widgetsDestDir -ItemType Directory | Out-Null
    }
    Copy-Item -Path "$widgetsSrcDir\*" -Destination $widgetsDestDir -Recurse -Force

    $filesToDelete = @('*.pdb', '*.deps.json')

    foreach ($filePattern in $filesToDelete) {
        Get-ChildItem -Path $targetDir -Recurse -Filter $filePattern | ForEach-Object { Remove-Item -Path $_.FullName -Force }
    }
}