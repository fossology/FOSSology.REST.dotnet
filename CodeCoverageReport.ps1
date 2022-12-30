# -------------------------------------------------------
# Create Code Covergae Report - Powershell based approach
# -------------------------------------------------------

$testfolder = "./.tests"

# 1. Remove all TestResult folders and files
Write-Host "Removing all 'TestResults' folders..."
$testfolders = Get-ChildItem -Path . "TestResults" -Recurse -Directory
foreach ($folder in $testfolders) {
    Remove-Item $folder.FullName -Recurse
}

Write-Host "Removing all 'coverage.cobertura.xml' files..."
$coberturaFiles = Get-ChildItem -Path . "coverage.cobertura.xml" -Recurse
foreach ($file in $coberturaFiles) {
    Remove-Item $file.FullName
}

# 2. Run tests
Write-Host "Running unit tests..."
# OLD way
# dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura
# new way
dotnet test --collect:"XPlat Code Coverage"

# 3 Generate reports
Write-Host "Generating reports..."
$coberturaFiles = Get-ChildItem -Path . "coverage.cobertura.xml" -Recurse
$list = ""
for ($i=0; $i -lt $coberturaFiles.Count; $i++) {
    $list = $list + $coberturaFiles[$i].FullName
    $list = $list + ";"
}

$cmd = "-reports:" + $list
$targetdir = "-targetdir:" + $testfolder + "\coverage-report"
reportgenerator $cmd $targetdir -reporttypes:"Html"

$targetdir = "-targetdir:" + $testfolder + "\badges"
reportgenerator $cmd $targetdir -reporttypes:"Badges"

$targetdir = "-targetdir:" + $testfolder
reportgenerator $cmd $targetdir -reporttypes:"Cobertura;JsonSummary"
