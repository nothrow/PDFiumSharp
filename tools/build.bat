pushd %~dp0\..

dotnet build -c Release dirs.proj
dotnet pack -c Release --no-build src\nothrow.PDFium
dotnet pack -c Release --no-build src\nothrow.PDFium.Binaries

popd