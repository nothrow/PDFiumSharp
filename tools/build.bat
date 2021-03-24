pushd %~dp0\..


dotnet restore

dotnet build -c Release src\nothrow.PDFium.Binaries
dotnet build -c Release src\nothrow.PDFium

dotnet pack -c Release src\nothrow.PDFium
dotnet pack -c Release src\nothrow.PDFium.Binaries

popd