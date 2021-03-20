# PDFiumSharp Library

The PDFiumSharp library is a C#/.NET core wrapper around the <a href="https://pdfium.googlesource.com/pdfium/">PDFium</a> library. It enables .NET developers to create, open, manipulate, render and save PDF documents.

This fork is focused on providing support for PDFium on .net core in containerized environment. Linux version is must-have.

## Getting Started

The easiest way to get going is to reference the NuGet packages. There is single NuGet package available:

- [nothrow.PDFium](https://www.nuget.org/packages/nothrow.PDFium/) - contains Linux, Windows x64 + x86 packages. 

## Development

For building, just `dotnet build` in root is enough.

As you see, there is no solution. You want solution? Run `tools/solution.bat`.
