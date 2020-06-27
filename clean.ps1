# -------------
# Clean project
# -------------

dotnet clean
Remove-Item "Fossology.Rest.Dotnet\bin" -Recurse -ErrorAction SilentlyContinue
Remove-Item "Fossology.Rest.Dotnet\obj" -Recurse -ErrorAction SilentlyContinue

Remove-Item "Fossology.Rest.Dotnet.Model\bin" -Recurse -ErrorAction SilentlyContinue
Remove-Item "Fossology.Rest.Dotnet.Model\obj" -Recurse -ErrorAction SilentlyContinue

Remove-Item "Fossology.Rest.Dotnet.Test\bin" -Recurse -ErrorAction SilentlyContinue
Remove-Item "Fossology.Rest.Dotnet.Test\obj" -Recurse -ErrorAction SilentlyContinue

Remove-Item "FossyApiDemo\bin" -Recurse -ErrorAction SilentlyContinue
Remove-Item "FossyApiDemo\obj" -Recurse -ErrorAction SilentlyContinue
