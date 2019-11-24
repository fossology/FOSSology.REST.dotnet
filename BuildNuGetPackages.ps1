# ======================
# = Build Nuget packages
# ======================

# Build Nuget packges

# 2019-11-24, T. Graf

# global version
$version = "1.0.0"
$copyright = "Copyright (C) 2019 T. Graf"
$all = "version=" + $version + ";copyright=" + $copyright

cd Fossology.Rest.Dotnet.Model
nuget pack Fossology.Rest.Dotnet.Model.nuspec -properties $all
cd ..

cd Fossology.Rest.Dotnet
nuget pack Fossology.Rest.Dotnet.nuspec -properties $all
cd ..

# ===============================
# ===============================