#region Copyright and License
/*
This file is part of PDFiumSharp, a wrapper around the PDFium library for the .NET framework.
Copyright (C) 2017 Tobias Meyer
License: Microsoft Reciprocal License (MS-RL)
*/
#endregion
using System;
using System.Collections.Generic;
using System.Text;
using PDFiumSharp.Types;
using System.Runtime.InteropServices;

namespace PDFiumSharp
{
    public sealed class PdfPage : NativeWrapper<FPDF_PAGE>
    {
		/// <summary>
		/// Gets the page width (excluding non-displayable area) measured in points.
		/// One point is 1/72 inch(around 0.3528 mm).
		/// </summary>
		public double Width => PDFiumInterop.FPDF_GetPageWidth(Handle);

		/// <summary>
		/// Gets the page height (excluding non-displayable area) measured in points.
		/// One point is 1/72 inch(around 0.3528 mm).
		/// </summary>
		public double Height => PDFiumInterop.FPDF_GetPageHeight(Handle);

		/// <summary>
		/// Gets the page width and height (excluding non-displayable area) measured in points.
		/// One point is 1/72 inch(around 0.3528 mm).
		/// </summary>
		public (double Width, double Height) Size
		{
			get
			{
				if (PDFiumInterop.FPDF_GetPageSizeByIndex(Document.Handle, Index, out var width, out var height))
					return (width, height);
				throw new PDFiumException();
			}
		}

		/// <summary>
		/// Gets the page orientation.
		/// </summary>
		public PageOrientations Orientation
		{
			get => PDFiumInterop.FPDFPage_GetRotation(Handle);
			set => PDFiumInterop.FPDFPage_SetRotation(Handle, value);
		}

        /// <summary>
        /// Get the transparency of the page
        /// </summary>
        public bool HasTransparency => PDFiumInterop.FPDFPage_HasTransparency(Handle);

        /// <summary>
        /// Gets the zero-based index of the page in the <see cref="Document"/>
        /// </summary>
        public int Index { get; internal set; } = -1;

		/// <summary>
		/// Gets the <see cref="PdfDocument"/> which contains the page.
		/// </summary>
		public PdfDocument Document { get; }

		//public string Label => PDFium.FPDF_GetPageLabel(Document.Handle, Index);

		PdfPage(PdfDocument doc, FPDF_PAGE page, int index)
			: base(page)
		{
			if (page.IsNull)
				throw new PDFiumException();
			Document = doc;
			Index = index;
		}

		internal static PdfPage Load(PdfDocument doc, int index) => new PdfPage(doc, PDFiumInterop.FPDF_LoadPage(doc.Handle, index), index);
		internal static PdfPage New(PdfDocument doc, int index, double width, double height) => new PdfPage(doc, PDFiumInterop.FPDFPage_New(doc.Handle, index, width, height), index);

		/// <summary>
		/// Renders the page to a <see cref="PDFiumBitmap"/>
		/// </summary>
		/// <param name="renderTarget">The bitmap to which the page is to be rendered.</param>
		/// <param name="rectDest">The destination rectangle in <paramref name="renderTarget"/>.</param>
		/// <param name="orientation">The orientation at which the page is to be rendered.</param>
		/// <param name="flags">The flags specifying how the page is to be rendered.</param>
		public void Render(PDFiumBitmap renderTarget, (int left, int top, int width, int height) rectDest, PageOrientations orientation = PageOrientations.Normal, RenderingFlags flags = RenderingFlags.None)
		{
			if (renderTarget == null)
				throw new ArgumentNullException(nameof(renderTarget));

			PDFiumInterop.FPDF_RenderPageBitmap(renderTarget.Handle, Handle, rectDest.left, rectDest.top, rectDest.width, rectDest.height, orientation, flags);
		}

		/// <summary>
		/// Renders the page to a <see cref="PDFiumBitmap"/>
		/// </summary>
		/// <param name="renderTarget">The bitmap to which the page is to be rendered.</param>
		/// <param name="orientation">The orientation at which the page is to be rendered.</param>
		/// <param name="flags">The flags specifying how the page is to be rendered.</param>
		public void Render(PDFiumBitmap renderTarget, PageOrientations orientation = PageOrientations.Normal, RenderingFlags flags = RenderingFlags.None)
		{
			Render(renderTarget, (0, 0, renderTarget.Width, renderTarget.Height), orientation, flags);
		}

		public (double X, double Y) DeviceToPage((int left, int top, int width, int height) displayArea, int deviceX, int deviceY, PageOrientations orientation = PageOrientations.Normal)
		{
			(var left, var top, var width, var height) = displayArea;
			PDFiumInterop.FPDF_DeviceToPage(Handle, left, top, width, height, orientation, deviceX, deviceY, out var x, out var y);
			return (x, y);
		}

		public (int X, int Y) PageToDevice((int left, int top, int width, int height) displayArea, double pageX, double pageY, PageOrientations orientation = PageOrientations.Normal)
		{
			(var left, var top, var width, var height) = displayArea;
			PDFiumInterop.FPDF_PageToDevice(Handle, left, top, width, height, orientation, pageX, pageY, out var x, out var y);
			return (x, y);
		}

		public FlattenResults Flatten(FlattenFlags flags) => PDFiumInterop.FPDFPage_Flatten(Handle, flags);

        public void Dispose() => ((IDisposable)this).Dispose();

        protected override void Dispose(FPDF_PAGE handle)
		{
			PDFiumInterop.FPDF_ClosePage(handle);
		}
	}
}
