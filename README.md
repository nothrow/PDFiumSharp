# nothrow.PDFium Library

[![NuGet version (nothrow.PDFium)](https://img.shields.io/nuget/v/nothrow.PDFium.svg?style=flat-square)](https://www.nuget.org/packages/nothrow.PDFium/)

The PDFiumSharp library is a C#/.NET core wrapper around the <a href="https://pdfium.googlesource.com/pdfium/">PDFium</a> library. It enables .NET developers to create, open, manipulate, render and save PDF documents.

This fork is focused on providing support for PDFium on .net core in containerized environment. Linux version is must-have. Binaries are taken during build from [https://github.com/bblanchon/pdfium-binaries/](https://github.com/bblanchon/pdfium-binaries/).


## Getting Started

The easiest way to get going is to reference the NuGet packages. There is single NuGet package available:

- [nothrow.PDFium](https://www.nuget.org/packages/nothrow.PDFium/) - contains wrapper, depends on `nothrow.PDFium.Binaries`
- [nothrow.PDFium.Binaries](https://www.nuget.org/packages/nothrow.PDFium.Binaries/) - contains Linux, Windows, macOS compiled versions of pdfium.

## Versioning

The package itself occupies only major.minor versions. On any significant update, minor is increased.
pdfium build is signalized by the .release. version. So, for example, `1.0.4666` nuget says it is `1.0` version of the wrapper, containing binaries from `4666` build of pdfium.

## Development

For building, just `dotnet build` in root is enough.

As you see, there is no solution. You want solution? Run `tools/solution.bat`.
