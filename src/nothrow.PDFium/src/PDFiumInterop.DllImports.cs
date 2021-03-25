#region Copyright and License
/*
This file is part of PDFiumSharp, a wrapper around the PDFium library for the .NET framework.
Copyright (C) 2017 Tobias Meyer
License: Microsoft Reciprocal License (MS-RL)
*/
#endregion

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using PDFiumSharp.Types;

namespace PDFiumSharp
{
    public static partial class PDFium
    {
        // nice. the library is not reentrant, nor thread safe. we need to lock all accesses.
        private static readonly object _syncroot = new object();
        public static void FPDF_InitLibrary()
        {
            lock (_syncroot) Interop.FPDF_InitLibrary();
        }

        public static void FPDF_InitLibraryWithConfig(ref FPDF_LIBRARY_CONFIG config)
        {
            lock (_syncroot) Interop.FPDF_InitLibraryWithConfig(ref config);
        }


        public static void FPDF_DestroyLibrary()
        {
            lock (_syncroot) Interop.FPDF_DestroyLibrary();
        }


        public static FPDF_DOCUMENT FPDF_LoadDocument(string file_path,
            string password)
        {
            lock (_syncroot) return Interop.FPDF_LoadDocument(file_path, password);
        }


        private static FPDF_DOCUMENT FPDF_LoadMemDocument(ref byte data_buf, int size,
            string password)
        {
            lock (_syncroot) return Interop.FPDF_LoadMemDocument(ref data_buf, size, password);
        }


        public static FPDF_DOCUMENT FPDF_LoadCustomDocument(FPDF_FILEREAD fileRead,
            string password)
        {
            lock (_syncroot) return Interop.FPDF_LoadCustomDocument(fileRead, password);
        }


        public static bool FPDF_GetFileVersion(FPDF_DOCUMENT doc, out int fileVersion)
        {
            lock (_syncroot) return Interop.FPDF_GetFileVersion(doc, out fileVersion);
        }


        public static FPDF_ERR FPDF_GetLastError()
        {
            lock (_syncroot) return Interop.FPDF_GetLastError();
        }


        public static DocumentPermissions FPDF_GetDocPermissions(FPDF_DOCUMENT document)
        {
            lock (_syncroot) return Interop.FPDF_GetDocPermissions(document);
        }


        public static int FPDF_GetSecurityHandlerRevision(FPDF_DOCUMENT document)
        {
            lock (_syncroot) return Interop.FPDF_GetSecurityHandlerRevision(document);
        }


        public static int FPDF_GetPageCount(FPDF_DOCUMENT document)
        {
            lock (_syncroot) return Interop.FPDF_GetPageCount(document);
        }



        public static FPDF_PAGE FPDF_LoadPage(FPDF_DOCUMENT document, int page_index)
        {
            lock (_syncroot) return Interop.FPDF_LoadPage(document, page_index);
        }


        public static double FPDF_GetPageWidth(FPDF_PAGE page)
        {
            lock (_syncroot) return Interop.FPDF_GetPageWidth(page);
        }


        public static double FPDF_GetPageHeight(FPDF_PAGE page)
        {
            lock (_syncroot) return Interop.FPDF_GetPageHeight(page);
        }


        public static bool FPDF_GetPageSizeByIndex(FPDF_DOCUMENT document, int page_index, out double width,
            out double height)
        {
            lock (_syncroot) return Interop.FPDF_GetPageSizeByIndex(document, page_index, out width, out height);
        }


        public static void FPDF_RenderPageBitmap(FPDF_BITMAP bitmap, FPDF_PAGE page, int start_x,
            int start_y, int size_x, int size_y, PageOrientations rotation, RenderingFlags flags)
        {
            lock (_syncroot) Interop.FPDF_RenderPageBitmap(bitmap, page, start_x, start_y, size_x, size_y, rotation, flags);
        }

        public static void FPDF_RenderPageBitmapWithMatrix(FPDF_BITMAP bitmap, FPDF_PAGE page, FS_MATRIX matrix, FS_RECTF clipping, RenderingFlags flags)
        {
            lock (_syncroot) Interop.FPDF_RenderPageBitmapWithMatrix(bitmap, page, matrix, clipping, flags);
        }
        public static void FPDF_ClosePage(FPDF_PAGE page)
        {
            lock (_syncroot) Interop.FPDF_ClosePage(page);
        }
        public static void FPDF_CloseDocument(FPDF_DOCUMENT document)
        {
            lock (_syncroot) Interop.FPDF_CloseDocument(document);
        }
        public static void FPDF_DeviceToPage(FPDF_PAGE page, int start_x, int start_y, int size_x, int size_y, PageOrientations rotate, int device_x, int device_y, out double page_x, out double page_y)
        {
            lock (_syncroot) Interop.FPDF_DeviceToPage(page, start_x, start_y, size_x, size_y, rotate, device_x, device_y, out page_x, out page_y);
        }
        public static void FPDF_PageToDevice(FPDF_PAGE page, int start_x, int start_y, int size_x, int size_y, PageOrientations rotate, double page_x, double page_y, out int device_x, out int device_y)
        {
            lock (_syncroot) Interop.FPDF_PageToDevice(page, start_x, start_y, size_x, size_y, rotate, page_x, page_y, out device_x, out device_y);
        }
        public static FPDF_BITMAP FPDFBitmap_Create(int width, int height, bool hasAlpha)
        {
            lock (_syncroot) return Interop.FPDFBitmap_Create(width, height, hasAlpha);
        }
        public static FPDF_BITMAP FPDFBitmap_CreateEx(int width, int height, BitmapFormats format, IntPtr first_scan, int stride)
        {
            lock (_syncroot) return Interop.FPDFBitmap_CreateEx(width, height, format, first_scan, stride);
        }
        public static void FPDFBitmap_FillRect(FPDF_BITMAP bitmap, int left, int top, int width, int height, FPDF_COLOR color)
        {
            lock (_syncroot) Interop.FPDFBitmap_FillRect(bitmap, left, top, width, height, color);
        }
        public static IntPtr FPDFBitmap_GetBuffer(FPDF_BITMAP bitmap)
        {
            lock (_syncroot) return Interop.FPDFBitmap_GetBuffer(bitmap);
        }
        public static int FPDFBitmap_GetWidth(FPDF_BITMAP bitmap)
        {
            lock (_syncroot) return Interop.FPDFBitmap_GetWidth(bitmap);
        }
        public static int FPDFBitmap_GetHeight(FPDF_BITMAP bitmap)
        {
            lock (_syncroot) return Interop.FPDFBitmap_GetHeight(bitmap);
        }
        public static int FPDFBitmap_GetStride(FPDF_BITMAP bitmap)
        {
            lock (_syncroot) return Interop.FPDFBitmap_GetStride(bitmap);
        }
        public static void FPDFBitmap_Destroy(FPDF_BITMAP bitmap)
        {
            lock (_syncroot) Interop.FPDFBitmap_Destroy(bitmap);
        }
        public static bool FPDF_VIEWERREF_GetPrintScaling(FPDF_DOCUMENT document)
        {
            lock (_syncroot) return Interop.FPDF_VIEWERREF_GetPrintScaling(document);
        }
        public static int FPDF_VIEWERREF_GetNumCopies(FPDF_DOCUMENT document)
        {
            lock (_syncroot) return Interop.FPDF_VIEWERREF_GetNumCopies(document);
        }
        public static FPDF_PAGERANGE FPDF_VIEWERREF_GetPrintPageRange(FPDF_DOCUMENT document)
        {
            lock (_syncroot) return Interop.FPDF_VIEWERREF_GetPrintPageRange(document);
        }
        public static DuplexTypes FPDF_VIEWERREF_GetDuplex(FPDF_DOCUMENT document)
        {
            lock (_syncroot) return Interop.FPDF_VIEWERREF_GetDuplex(document);
        }
        private static uint FPDF_VIEWERREF_GetName(FPDF_DOCUMENT document, string key, ref byte buffer, uint length)
        {
            lock (_syncroot) return Interop.FPDF_VIEWERREF_GetName(document, key, ref buffer, length);
        }
        public static int FPDF_CountNamedDests(FPDF_DOCUMENT document)
        {
            lock (_syncroot) return Interop.FPDF_CountNamedDests(document);
        }
        public static FPDF_DEST FPDF_GetNamedDestByName(FPDF_DOCUMENT document, string name)
        {
            lock (_syncroot) return Interop.FPDF_GetNamedDestByName(document, name);
        }
        private static FPDF_DEST FPDF_GetNamedDest(FPDF_DOCUMENT document, int index, IntPtr buffer, out int buflen)
        {
            lock (_syncroot) return Interop.FPDF_GetNamedDest(document, index, buffer, out buflen);
        }
        private static FPDF_DEST FPDF_GetNamedDest(FPDF_DOCUMENT document, int index, ref byte buffer, ref int buflen)
        {
            lock (_syncroot) return Interop.FPDF_GetNamedDest(document, index, ref buffer, ref buflen);
        }

        public static FPDF_BOOKMARK FPDFBookmark_GetFirstChild(FPDF_DOCUMENT document, FPDF_BOOKMARK bookmark)
        {
            lock (_syncroot) return Interop.FPDFBookmark_GetFirstChild(document, bookmark);
        }
        public static FPDF_BOOKMARK FPDFBookmark_GetNextSibling(FPDF_DOCUMENT document, FPDF_BOOKMARK bookmark)
        {
            lock (_syncroot) return Interop.FPDFBookmark_GetNextSibling(document, bookmark);
        }
        private static uint FPDFBookmark_GetTitle(FPDF_BOOKMARK bookmark, ref byte buffer, uint buflen)
        {
            lock (_syncroot) return Interop.FPDFBookmark_GetTitle(bookmark, ref buffer, buflen);
        }
        public static FPDF_BOOKMARK FPDFBookmark_Find(FPDF_DOCUMENT document, string title)
        {
            lock (_syncroot) return Interop.FPDFBookmark_Find(document, title);
        }
        public static FPDF_DEST FPDFBookmark_GetDest(FPDF_DOCUMENT document, FPDF_BOOKMARK bookmark)
        {
            lock (_syncroot) return Interop.FPDFBookmark_GetDest(document, bookmark);
        }
        public static FPDF_ACTION FPDFBookmark_GetAction(FPDF_BOOKMARK bookmark)
        {
            lock (_syncroot) return Interop.FPDFBookmark_GetAction(bookmark);
        }
        public static ActionTypes FPDFAction_GetType(FPDF_ACTION action)
        {
            lock (_syncroot) return Interop.FPDFAction_GetType(action);
        }
        public static FPDF_DEST FPDFAction_GetDest(FPDF_DOCUMENT document, FPDF_ACTION action)
        {
            lock (_syncroot) return Interop.FPDFAction_GetDest(document, action);
        }
        private static uint FPDFAction_GetFilePath(FPDF_ACTION action, ref byte buffer, uint buflen)
        {
            lock (_syncroot) return Interop.FPDFAction_GetFilePath(action, ref buffer, buflen);
        }
        private static uint FPDFAction_GetURIPath(FPDF_DOCUMENT document, FPDF_ACTION action, ref byte buffer, uint buflen)
        {
            lock (_syncroot) return Interop.FPDFAction_GetURIPath(document, action, ref buffer, buflen);
        }
        public static int FPDFDest_GetPageIndex(FPDF_DOCUMENT document, FPDF_DEST dest)
        {
            lock (_syncroot) return Interop.FPDFDest_GetPageIndex(document, dest);
        }
        public static bool FPDFDest_GetLocationInPage(FPDF_DEST dest, out bool hasXCoord, out bool hasYCoord, out bool hasZoom, out float x, out float y, out float zoom)
        {
            lock (_syncroot) return Interop.FPDFDest_GetLocationInPage(dest, out hasXCoord, out hasYCoord, out hasZoom, out x, out y, out zoom);
        }
        public static FPDF_LINK FPDFLink_GetLinkAtPoint(FPDF_PAGE page, double x, double y)
        {
            lock (_syncroot) return Interop.FPDFLink_GetLinkAtPoint(page, x, y);
        }
        public static int FPDFLink_GetLinkZOrderAtPoint(FPDF_PAGE page, double x, double y)
        {
            lock (_syncroot) return Interop.FPDFLink_GetLinkZOrderAtPoint(page, x, y);
        }
        public static FPDF_DEST FPDFLink_GetDest(FPDF_DOCUMENT document, FPDF_LINK link)
        {
            lock (_syncroot) return Interop.FPDFLink_GetDest(document, link);
        }
        public static FPDF_ACTION FPDFLink_GetAction(FPDF_LINK link)
        {
            lock (_syncroot) return Interop.FPDFLink_GetAction(link);
        }
        private static bool FPDFLink_Enumerate(FPDF_PAGE page, ref int startPos, out FPDF_LINK linkAnnot)
        {
            lock (_syncroot) return Interop.FPDFLink_Enumerate(page, ref startPos, out linkAnnot);
        }
        public static bool FPDFLink_GetAnnotRect(FPDF_LINK linkAnnot, out FS_RECTF rect)
        {
            lock (_syncroot) return Interop.FPDFLink_GetAnnotRect(linkAnnot, out rect);
        }
        public static int FPDFLink_CountQuadPoints(FPDF_LINK linkAnnot)
        {
            lock (_syncroot) return Interop.FPDFLink_CountQuadPoints(linkAnnot);
        }
        public static bool FPDFLink_GetQuadPoints(FPDF_LINK linkAnnot, int quadIndex, out FS_QUADPOINTSF quadPoints)
        {
            lock (_syncroot) return Interop.FPDFLink_GetQuadPoints(linkAnnot, quadIndex, out quadPoints);
        }


        public static uint FPDF_GetMetaText(FPDF_DOCUMENT document, string tag, ref byte buffer, uint buflen)
        {
            lock (_syncroot) return Interop.FPDF_GetMetaText(document, tag, ref buffer, buflen);
        }
        private static uint FPDF_GetPageLabel(FPDF_DOCUMENT document, int page_index, ref byte buffer, uint buflen)
        {
            lock (_syncroot) return Interop.FPDF_GetPageLabel(document, page_index, ref buffer, buflen);
        }
        public static FPDF_DOCUMENT FPDF_CreateNewDocument()
        {
            lock (_syncroot) return Interop.FPDF_CreateNewDocument();
        }
        public static FPDF_PAGE FPDFPage_New(FPDF_DOCUMENT document, int page_index, double width, double height)
        {
            lock (_syncroot) return Interop.FPDFPage_New(document, page_index, width, height);
        }
        public static void FPDFPage_Delete(FPDF_DOCUMENT document, int page_index)
        {
            lock (_syncroot) Interop.FPDFPage_Delete(document, page_index);
        }
        public static PageOrientations FPDFPage_GetRotation(FPDF_PAGE page)
        {
            lock (_syncroot) return Interop.FPDFPage_GetRotation(page);
        }
        public static void FPDFPage_SetRotation(FPDF_PAGE page, PageOrientations rotation)
        {
            lock (_syncroot) Interop.FPDFPage_SetRotation(page, rotation);
        }
        private static void FPDFPage_InsertObject(FPDF_PAGE page, FPDF_PAGEOBJECT page_obj)
        {
            lock (_syncroot) Interop.FPDFPage_InsertObject(page, page_obj);
        }
        public static int FPDFPage_CountObject(FPDF_PAGE page)
        {
            lock (_syncroot) return Interop.FPDFPage_CountObject(page);
        }
        public static FPDF_PAGEOBJECT FPDFPage_GetObject(FPDF_PAGE page, int index)
        {
            lock (_syncroot) return Interop.FPDFPage_GetObject(page, index);
        }
        public static bool FPDFPage_HasTransparency(FPDF_PAGE page)
        {
            lock (_syncroot) return Interop.FPDFPage_HasTransparency(page);
        }
        public static bool FPDFPage_GenerateContent(FPDF_PAGE page)
        {
            lock (_syncroot) return Interop.FPDFPage_GenerateContent(page);
        }
        public static bool FPDFPageObj_HasTransparency(FPDF_PAGEOBJECT pageObject)
        {
            lock (_syncroot) return Interop.FPDFPageObj_HasTransparency(pageObject);
        }
        public static void FPDFPageObj_Transform(FPDF_PAGEOBJECT page_object, double a, double b, double c, double d, double e, double f)
        {
            lock (_syncroot) Interop.FPDFPageObj_Transform(page_object, a, b, c, d, e, f);
        }
        public static void FPDFPage_TransformAnnots(FPDF_PAGE page, double a, double b, double c, double d, double e, double f)
        {
            lock (_syncroot) Interop.FPDFPage_TransformAnnots(page, a, b, c, d, e, f);
        }
        public static FPDF_PAGEOBJECT FPDFPageObj_NewImageObj(FPDF_DOCUMENT document)
        {
            lock (_syncroot) return Interop.FPDFPageObj_NewImageObj(document);
        }
        private static bool FPDFImageObj_LoadJpegFile(ref FPDF_PAGE pages, int nCount, FPDF_PAGEOBJECT image_object, FPDF_FILEREAD fileRead)
        {
            lock (_syncroot) return Interop.FPDFImageObj_LoadJpegFile(ref pages, nCount, image_object, fileRead);
        }
        private static bool FPDFImageObj_LoadJpegFileInline(ref FPDF_PAGE pages, int nCount, FPDF_PAGEOBJECT image_object, FPDF_FILEREAD fileRead)
        {
            lock (_syncroot) return Interop.FPDFImageObj_LoadJpegFileInline(ref pages, nCount, image_object, fileRead);
        }
        public static bool FPDFImageObj_SetMatrix(FPDF_PAGEOBJECT image_object, double a, double b, double c, double d, double e, double f)
        {
            lock (_syncroot) return Interop.FPDFImageObj_SetMatrix(image_object, a, b, c, d, e, f);
        }
        public static bool FPDFImageObj_SetBitmap(ref FPDF_PAGE pages, int nCount, FPDF_PAGEOBJECT image_object, FPDF_BITMAP bitmap)
        {
            lock (_syncroot) return Interop.FPDFImageObj_SetBitmap(ref pages, nCount, image_object, bitmap);
        }
        public static FPDF_PAGEOBJECT FPDFPageObj_CreateNewPath(float x, float y)
        {
            lock (_syncroot) return Interop.FPDFPageObj_CreateNewPath(x, y);
        }
        public static FPDF_PAGEOBJECT FPDFPageObj_CreateNewRect(float x, float y, float w, float h)
        {
            lock (_syncroot) return Interop.FPDFPageObj_CreateNewRect(x, y, w, h);
        }
        public static bool FPDFPath_SetStrokeColor(FPDF_PAGEOBJECT path, uint R, uint G, uint B, uint A)
        {
            lock (_syncroot) return Interop.FPDFPath_SetStrokeColor(path, R, G, B, A);
        }
        public static bool FPDFPath_SetStrokeWidth(FPDF_PAGEOBJECT path, float width)
        {
            lock (_syncroot) return Interop.FPDFPath_SetStrokeWidth(path, width);
        }
        public static bool FPDFPath_SetFillColor(FPDF_PAGEOBJECT path, uint R, uint G, uint B, uint A)
        {
            lock (_syncroot) return Interop.FPDFPath_SetFillColor(path, R, G, B, A);
        }
        public static bool FPDFPath_MoveTo(FPDF_PAGEOBJECT path, float x, float y)
        {
            lock (_syncroot) return Interop.FPDFPath_MoveTo(path, x, y);
        }
        public static bool FPDFPath_LineTo(FPDF_PAGEOBJECT path, float x, float y)
        {
            lock (_syncroot) return Interop.FPDFPath_LineTo(path, x, y);
        }
        public static bool FPDFPath_BezierTo(FPDF_PAGEOBJECT path, float x1, float y1, float x2, float y2, float x3, float y3)
        {
            lock (_syncroot) return Interop.FPDFPath_BezierTo(path, x1, y1, x2, y2, x3, y3);
        }
        public static bool FPDFPath_Close(FPDF_PAGEOBJECT path)
        {
            lock (_syncroot) return Interop.FPDFPath_Close(path);
        }
        public static bool FPDFPath_SetDrawMode(FPDF_PAGEOBJECT path, PathFillModes fillmode, bool stroke)
        {
            lock (_syncroot) return Interop.FPDFPath_SetDrawMode(path, fillmode, stroke);
        }
        public static FPDF_PAGEOBJECT FPDFPageObj_NewTextObj(FPDF_DOCUMENT document, string font, float font_size)
        {
            lock (_syncroot) return Interop.FPDFPageObj_NewTextObj(document, font, font_size);
        }



        public static bool FPDFText_SetText(FPDF_PAGEOBJECT text_object, string text)
        {
            lock (_syncroot) return Interop.FPDFText_SetText(text_object, text);
        }
        private static FPDF_FONT FPDFText_LoadFont(FPDF_DOCUMENT document, ref byte data, uint size, FontTypes font_type, bool cid)
        {
            lock (_syncroot) return Interop.FPDFText_LoadFont(document, ref data, size, font_type, cid);
        }
        public static PageModes FPDFDoc_GetPageMode(FPDF_DOCUMENT document)
        {
            lock (_syncroot) return Interop.FPDFDoc_GetPageMode(document);
        }
        public static FlattenResults FPDFPage_Flatten(FPDF_PAGE page, FlattenFlags nFlag)
        {
            lock (_syncroot) return Interop.FPDFPage_Flatten(page, nFlag);
        }
        public static bool FPDF_ImportPages(FPDF_DOCUMENT dest_doc, FPDF_DOCUMENT src_doc, string pagerange, int index)
        {
            lock (_syncroot) return Interop.FPDF_ImportPages(dest_doc, src_doc, pagerange, index);
        }
        public static bool FPDF_CopyViewerPreferences(FPDF_DOCUMENT dest_doc, FPDF_DOCUMENT src_doc)
        {
            lock (_syncroot) return Interop.FPDF_CopyViewerPreferences(dest_doc, src_doc);
        }
        public static RenderingStatus FPDF_RenderPageBitmap_Start(FPDF_BITMAP bitmap, FPDF_PAGE page, int start_x, int start_y, int size_x, int size_y, PageOrientations rotate, RenderingFlags flags, IFSDK_PAUSE pause)
        {
            lock (_syncroot) return Interop.FPDF_RenderPageBitmap_Start(bitmap, page, start_x, start_y, size_x, size_y, rotate, flags, pause);
        }
        public static RenderingStatus FPDF_RenderPage_Continue(FPDF_PAGE page, IFSDK_PAUSE pause)
        {
            lock (_syncroot) return Interop.FPDF_RenderPage_Continue(page, pause);
        }
        public static void FPDF_RenderPage_Close(FPDF_PAGE page)
        {
            lock (_syncroot) Interop.FPDF_RenderPage_Close(page);
        }
        public static bool FPDF_SaveAsCopy(FPDF_DOCUMENT document, FPDF_FILEWRITE fileWrite, SaveFlags flags)
        {
            lock (_syncroot) return Interop.FPDF_SaveAsCopy(document, fileWrite, flags);
        }
        public static bool FPDF_SaveWithVersion(FPDF_DOCUMENT document, FPDF_FILEWRITE fileWrite, SaveFlags flags, int fileVersion)
        {
            lock (_syncroot) return Interop.FPDF_SaveWithVersion(document, fileWrite, flags, fileVersion);
        }
        public static int FPDFText_GetCharIndexFromTextIndex(FPDF_TEXTPAGE text_page, int nTextIndex)
        {
            lock (_syncroot) return Interop.FPDFText_GetCharIndexFromTextIndex(text_page, nTextIndex);
        }
        public static int FPDFText_GetTextIndexFromCharIndex(FPDF_TEXTPAGE text_page, int nCharIndex)
        {
            lock (_syncroot) return Interop.FPDFText_GetTextIndexFromCharIndex(text_page, nCharIndex);
        }
        public static FPDF_STRUCTTREE FPDF_StructTree_GetForPage(FPDF_PAGE page)
        {
            lock (_syncroot) return Interop.FPDF_StructTree_GetForPage(page);
        }
        public static void FPDF_StructTree_Close(FPDF_STRUCTTREE struct_tree)
        {
            lock (_syncroot) Interop.FPDF_StructTree_Close(struct_tree);
        }
        public static int FPDF_StructTree_CountChildren(FPDF_STRUCTTREE struct_tree)
        {
            lock (_syncroot) return Interop.FPDF_StructTree_CountChildren(struct_tree);
        }
        public static FPDF_STRUCTELEMENT FPDF_StructTree_GetChildAtIndex(FPDF_STRUCTTREE struct_tree, int index)
        {
            lock (_syncroot) return Interop.FPDF_StructTree_GetChildAtIndex(struct_tree, index);
        }
        private static uint FPDF_StructElement_GetAltText(FPDF_STRUCTELEMENT struct_element, ref byte buffer, uint buflen)
        {
            lock (_syncroot) return Interop.FPDF_StructElement_GetAltText(struct_element, ref buffer, buflen);
        }
        public static int FPDF_StructElement_CountChildren(FPDF_STRUCTELEMENT struct_element)
        {
            lock (_syncroot) return Interop.FPDF_StructElement_CountChildren(struct_element);
        }
        public static FPDF_STRUCTELEMENT FPDF_StructElement_GetChildAtIndex(FPDF_STRUCTELEMENT struct_element, int index)
        {
            lock (_syncroot) return Interop.FPDF_StructElement_GetChildAtIndex(struct_element, index);
        }
        public static FPDF_TEXTPAGE FPDFText_LoadPage(FPDF_PAGE page)
        {
            lock (_syncroot) return Interop.FPDFText_LoadPage(page);
        }
        public static void FPDFText_ClosePage(FPDF_TEXTPAGE text_page)
        {
            lock (_syncroot) Interop.FPDFText_ClosePage(text_page);
        }
        public static int FPDFText_CountChars(FPDF_TEXTPAGE text_page)
        {
            lock (_syncroot) return Interop.FPDFText_CountChars(text_page);
        }
        public static char FPDFText_GetUnicode(FPDF_TEXTPAGE text_page, int index)
        {
            lock (_syncroot) return Interop.FPDFText_GetUnicode(text_page, index);
        }
        public static double FPDFText_GetFontSize(FPDF_TEXTPAGE text_page, int index)
        {
            lock (_syncroot) return Interop.FPDFText_GetFontSize(text_page, index);
        }
        public static void FPDFText_GetCharBox(FPDF_TEXTPAGE text_page, int index, out double left, out double right, out double bottom, out double top)
        {
            lock (_syncroot) Interop.FPDFText_GetCharBox(text_page, index, out left, out right, out bottom, out top);
        }





        public static int FPDFText_GetCharIndexAtPos(FPDF_TEXTPAGE text_page, double x, double y, double xTolerance, double yTolerance)
        {
            lock (_syncroot) return Interop.FPDFText_GetCharIndexAtPos(text_page, x, y, xTolerance, yTolerance);
        }
        private static int FPDFText_GetText(FPDF_TEXTPAGE text_page, int start_index, int count, ref byte result)
        {
            lock (_syncroot) return Interop.FPDFText_GetText(text_page, start_index, count, ref result);
        }
        public static int FPDFText_CountRects(FPDF_TEXTPAGE text_page, int start_index, int count)
        {
            lock (_syncroot) return Interop.FPDFText_CountRects(text_page, start_index, count);
        }
        public static void FPDFText_GetRect(FPDF_TEXTPAGE text_page, int rect_index, out double left, out double top, out double right, out double bottom)
        {
            lock (_syncroot) Interop.FPDFText_GetRect(text_page, rect_index, out left, out top, out right, out bottom);
        }
        private static int FPDFText_GetBoundedText(FPDF_TEXTPAGE text_page, double left, double top, double right, double bottom, ref byte buffer, int buflen)
        {
            lock (_syncroot) return Interop.FPDFText_GetBoundedText(text_page, left, top, right, bottom, ref buffer, buflen);
        }
        public static FPDF_SCHHANDLE FPDFText_FindStart(FPDF_TEXTPAGE text_page, string findwhat, SearchFlags flags, int start_index)
        {
            lock (_syncroot) return Interop.FPDFText_FindStart(text_page, findwhat, flags, start_index);
        }
        public static bool FPDFText_FindNext(FPDF_SCHHANDLE handle)
        {
            lock (_syncroot) return Interop.FPDFText_FindNext(handle);
        }
        public static bool FPDFText_FindPrev(FPDF_SCHHANDLE handle)
        {
            lock (_syncroot) return Interop.FPDFText_FindPrev(handle);
        }
        public static int FPDFText_GetSchResultIndex(FPDF_SCHHANDLE handle)
        {
            lock (_syncroot) return Interop.FPDFText_GetSchResultIndex(handle);
        }
        public static int FPDFText_GetSchCount(FPDF_SCHHANDLE handle)
        {
            lock (_syncroot) return Interop.FPDFText_GetSchCount(handle);
        }
        public static void FPDFText_FindClose(FPDF_SCHHANDLE handle)
        {
            lock (_syncroot) Interop.FPDFText_FindClose(handle);
        }
        public static FPDF_PAGELINK FPDFLink_LoadWebLinks(FPDF_TEXTPAGE text_page)
        {
            lock (_syncroot) return Interop.FPDFLink_LoadWebLinks(text_page);
        }
        public static int FPDFLink_CountWebLinks(FPDF_PAGELINK link_page)
        {
            lock (_syncroot) return Interop.FPDFLink_CountWebLinks(link_page);
        }
        private static int FPDFLink_GetURL(FPDF_PAGELINK link_page, int link_index, ref byte buffer, int buflen)
        {
            lock (_syncroot) return Interop.FPDFLink_GetURL(link_page, link_index, ref buffer, buflen);
        }
        public static int FPDFLink_CountRects(FPDF_PAGELINK link_page, int link_index)
        {
            lock (_syncroot) return Interop.FPDFLink_CountRects(link_page, link_index);
        }
        public static void FPDFLink_GetRect(FPDF_PAGELINK link_page, int link_index, int rect_index, out double left, out double top, out double right, out double bottom)
        {
            lock (_syncroot) Interop.FPDFLink_GetRect(link_page, link_index, rect_index, out left, out top, out right, out bottom);
        }
        public static void FPDFLink_CloseWebLinks(FPDF_PAGELINK link_page)
        {
            lock (_syncroot) Interop.FPDFLink_CloseWebLinks(link_page);
        }
        public static void FPDFPage_SetMediaBox(FPDF_PAGE page, float left, float bottom, float right, float top)
        {
            lock (_syncroot) Interop.FPDFPage_SetMediaBox(page, left, bottom, right, top);
        }
        public static void FPDFPage_SetCropBox(FPDF_PAGE page, float left, float bottom, float right, float top)
        {
            lock (_syncroot) Interop.FPDFPage_SetCropBox(page, left, bottom, right, top);
        }
        public static bool FPDFPage_GetMediaBox(FPDF_PAGE page, out float left, out float bottom, out float right, out float top)
        {
            lock (_syncroot) return Interop.FPDFPage_GetMediaBox(page, out left, out bottom, out right, out top);
        }





        public static bool FPDFPage_GetCropBox(FPDF_PAGE page, out float left, out float bottom, out float right, out float top)
        {
            lock (_syncroot) return Interop.FPDFPage_GetCropBox(page, out left, out bottom, out right, out top);
        }



        public static bool FPDFPage_TransFormWithClip(FPDF_PAGE page,
            FS_MATRIX matrix,
            FS_RECTF clipRect)
        {
            lock (_syncroot) return Interop.FPDFPage_TransFormWithClip(page, matrix, clipRect);
        }



        public static void FPDFPageObj_TransformClipPath(FPDF_PAGEOBJECT page_object, double a, double b,
            double c, double d, double e, double f)
        {
            lock (_syncroot) Interop.FPDFPageObj_TransformClipPath(page_object, a, b, c, d, e, f);
        }



        public static FPDF_CLIPPATH FPDF_CreateClipPath(float left, float bottom, float right, float top)
        {
            lock (_syncroot) return Interop.FPDF_CreateClipPath(left, bottom, right, top);
        }


        public static void FPDF_DestroyClipPath(FPDF_CLIPPATH clipPath)
        {
            lock (_syncroot) Interop.FPDF_DestroyClipPath(clipPath);
        }


        public static void FPDFPage_InsertClipPath(FPDF_PAGE page, FPDF_CLIPPATH clipPath)
        {
            lock (_syncroot) Interop.FPDFPage_InsertClipPath(page, clipPath);
        }


        internal class Interop
        {

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDF_InitLibrary")]
            public static extern void FPDF_InitLibrary();

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall,
                EntryPoint = "FPDF_InitLibraryWithConfig")]
            public static extern void FPDF_InitLibraryWithConfig(ref FPDF_LIBRARY_CONFIG config);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDF_DestroyLibrary")]
            public static extern void FPDF_DestroyLibrary();

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDF_LoadDocument")]
            public static extern FPDF_DOCUMENT FPDF_LoadDocument([MarshalAs(UnmanagedType.LPStr)] string file_path,
                [MarshalAs(UnmanagedType.LPStr)] string password);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDF_LoadMemDocument")]
            internal static extern FPDF_DOCUMENT FPDF_LoadMemDocument(ref byte data_buf, int size,
                [MarshalAs(UnmanagedType.LPStr)] string password);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDF_LoadCustomDocument")]
            public static extern FPDF_DOCUMENT FPDF_LoadCustomDocument(FPDF_FILEREAD fileRead,
                [MarshalAs(UnmanagedType.LPStr)] string password);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDF_GetFileVersion")]
            public static extern bool FPDF_GetFileVersion(FPDF_DOCUMENT doc, out int fileVersion);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDF_GetLastError")]
            public static extern FPDF_ERR FPDF_GetLastError();

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDF_GetDocPermissions")]
            public static extern DocumentPermissions FPDF_GetDocPermissions(FPDF_DOCUMENT document);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall,
                EntryPoint = "FPDF_GetSecurityHandlerRevision")]
            public static extern int FPDF_GetSecurityHandlerRevision(FPDF_DOCUMENT document);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDF_GetPageCount")]
            public static extern int FPDF_GetPageCount(FPDF_DOCUMENT document);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDF_LoadPage")]
            public static extern FPDF_PAGE FPDF_LoadPage(FPDF_DOCUMENT document, int page_index);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDF_GetPageWidth")]
            public static extern double FPDF_GetPageWidth(FPDF_PAGE page);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDF_GetPageHeight")]
            public static extern double FPDF_GetPageHeight(FPDF_PAGE page);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDF_GetPageSizeByIndex")]
            public static extern bool FPDF_GetPageSizeByIndex(FPDF_DOCUMENT document, int page_index, out double width,
                out double height);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDF_RenderPageBitmap")]
            public static extern void FPDF_RenderPageBitmap(FPDF_BITMAP bitmap, FPDF_PAGE page, int start_x,
                int start_y, int size_x, int size_y, PageOrientations rotation, RenderingFlags flags);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall,
                EntryPoint = "FPDF_RenderPageBitmapWithMatrix")]
            public static extern void FPDF_RenderPageBitmapWithMatrix(FPDF_BITMAP bitmap, FPDF_PAGE page,
                [MarshalAs(UnmanagedType.LPStruct)] FS_MATRIX matrix,
                [MarshalAs(UnmanagedType.LPStruct)] FS_RECTF clipping, RenderingFlags flags);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDF_ClosePage")]
            public static extern void FPDF_ClosePage(FPDF_PAGE page);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDF_CloseDocument")]
            public static extern void FPDF_CloseDocument(FPDF_DOCUMENT document);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDF_DeviceToPage")]
            public static extern void FPDF_DeviceToPage(FPDF_PAGE page, int start_x, int start_y, int size_x,
                int size_y, PageOrientations rotate, int device_x, int device_y, out double page_x, out double page_y);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDF_PageToDevice")]
            public static extern void FPDF_PageToDevice(FPDF_PAGE page, int start_x, int start_y, int size_x,
                int size_y, PageOrientations rotate, double page_x, double page_y, out int device_x, out int device_y);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFBitmap_Create")]
            public static extern FPDF_BITMAP FPDFBitmap_Create(int width, int height, bool hasAlpha);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFBitmap_CreateEx")]
            public static extern FPDF_BITMAP FPDFBitmap_CreateEx(int width, int height, BitmapFormats format,
                IntPtr first_scan, int stride);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFBitmap_FillRect")]
            public static extern void FPDFBitmap_FillRect(FPDF_BITMAP bitmap, int left, int top, int width, int height,
                FPDF_COLOR color);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFBitmap_GetBuffer")]
            public static extern IntPtr FPDFBitmap_GetBuffer(FPDF_BITMAP bitmap);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFBitmap_GetWidth")]
            public static extern int FPDFBitmap_GetWidth(FPDF_BITMAP bitmap);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFBitmap_GetHeight")]
            public static extern int FPDFBitmap_GetHeight(FPDF_BITMAP bitmap);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFBitmap_GetStride")]
            public static extern int FPDFBitmap_GetStride(FPDF_BITMAP bitmap);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFBitmap_Destroy")]
            public static extern void FPDFBitmap_Destroy(FPDF_BITMAP bitmap);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall,
                EntryPoint = "FPDF_VIEWERREF_GetPrintScaling")]
            public static extern bool FPDF_VIEWERREF_GetPrintScaling(FPDF_DOCUMENT document);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall,
                EntryPoint = "FPDF_VIEWERREF_GetNumCopies")]
            public static extern int FPDF_VIEWERREF_GetNumCopies(FPDF_DOCUMENT document);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall,
                EntryPoint = "FPDF_VIEWERREF_GetPrintPageRange")]
            public static extern FPDF_PAGERANGE FPDF_VIEWERREF_GetPrintPageRange(FPDF_DOCUMENT document);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall,
                EntryPoint = "FPDF_VIEWERREF_GetDuplex")]
            public static extern DuplexTypes FPDF_VIEWERREF_GetDuplex(FPDF_DOCUMENT document);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDF_VIEWERREF_GetName")]
            public static extern uint FPDF_VIEWERREF_GetName(FPDF_DOCUMENT document,
                [MarshalAs(UnmanagedType.LPStr)] string key, ref byte buffer, uint length);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDF_CountNamedDests")]
            public static extern int FPDF_CountNamedDests(FPDF_DOCUMENT document);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDF_GetNamedDestByName")]
            public static extern FPDF_DEST FPDF_GetNamedDestByName(FPDF_DOCUMENT document,
                [MarshalAs(UnmanagedType.LPStr)] string name);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDF_GetNamedDest")]
            public static extern FPDF_DEST FPDF_GetNamedDest(FPDF_DOCUMENT document, int index, IntPtr buffer,
                out int buflen);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDF_GetNamedDest")]
            public static extern FPDF_DEST FPDF_GetNamedDest(FPDF_DOCUMENT document, int index, ref byte buffer,
                ref int buflen);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall,
                EntryPoint = "FPDFBookmark_GetFirstChild")]
            public static extern FPDF_BOOKMARK FPDFBookmark_GetFirstChild(FPDF_DOCUMENT document,
                FPDF_BOOKMARK bookmark);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall,
                EntryPoint = "FPDFBookmark_GetNextSibling")]
            public static extern FPDF_BOOKMARK FPDFBookmark_GetNextSibling(FPDF_DOCUMENT document,
                FPDF_BOOKMARK bookmark);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFBookmark_GetTitle")]
            public static extern uint FPDFBookmark_GetTitle(FPDF_BOOKMARK bookmark, ref byte buffer, uint buflen);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFBookmark_Find")]
            public static extern FPDF_BOOKMARK FPDFBookmark_Find(FPDF_DOCUMENT document,
                [MarshalAs(UnmanagedType.LPWStr)] string title);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFBookmark_GetDest")]
            public static extern FPDF_DEST FPDFBookmark_GetDest(FPDF_DOCUMENT document, FPDF_BOOKMARK bookmark);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFBookmark_GetAction")]
            public static extern FPDF_ACTION FPDFBookmark_GetAction(FPDF_BOOKMARK bookmark);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFAction_GetType")]
            public static extern ActionTypes FPDFAction_GetType(FPDF_ACTION action);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFAction_GetDest")]
            public static extern FPDF_DEST FPDFAction_GetDest(FPDF_DOCUMENT document, FPDF_ACTION action);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFAction_GetFilePath")]
            public static extern uint FPDFAction_GetFilePath(FPDF_ACTION action, ref byte buffer, uint buflen);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFAction_GetURIPath")]
            public static extern uint FPDFAction_GetURIPath(FPDF_DOCUMENT document, FPDF_ACTION action,
                ref byte buffer, uint buflen);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFDest_GetPageIndex")]
            public static extern int FPDFDest_GetPageIndex(FPDF_DOCUMENT document, FPDF_DEST dest);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall,
                EntryPoint = "FPDFDest_GetLocationInPage")]
            public static extern bool FPDFDest_GetLocationInPage(FPDF_DEST dest, out bool hasXCoord, out bool hasYCoord,
                out bool hasZoom, out float x, out float y, out float zoom);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFLink_GetLinkAtPoint")]
            public static extern FPDF_LINK FPDFLink_GetLinkAtPoint(FPDF_PAGE page, double x, double y);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall,
                EntryPoint = "FPDFLink_GetLinkZOrderAtPoint")]
            public static extern int FPDFLink_GetLinkZOrderAtPoint(FPDF_PAGE page, double x, double y);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFLink_GetDest")]
            public static extern FPDF_DEST FPDFLink_GetDest(FPDF_DOCUMENT document, FPDF_LINK link);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFLink_GetAction")]
            public static extern FPDF_ACTION FPDFLink_GetAction(FPDF_LINK link);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFLink_Enumerate")]
            public static extern bool FPDFLink_Enumerate(FPDF_PAGE page, ref int startPos, out FPDF_LINK linkAnnot);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFLink_GetAnnotRect")]
            public static extern bool FPDFLink_GetAnnotRect(FPDF_LINK linkAnnot, out FS_RECTF rect);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall,
                EntryPoint = "FPDFLink_CountQuadPoints")]
            public static extern int FPDFLink_CountQuadPoints(FPDF_LINK linkAnnot);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFLink_GetQuadPoints")]
            public static extern bool FPDFLink_GetQuadPoints(FPDF_LINK linkAnnot, int quadIndex,
                out FS_QUADPOINTSF quadPoints);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDF_GetMetaText")]
            public static extern uint FPDF_GetMetaText(FPDF_DOCUMENT document,
                [MarshalAs(UnmanagedType.LPStr)] string tag, ref byte buffer, uint buflen);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDF_GetPageLabel")]
            public static extern uint FPDF_GetPageLabel(FPDF_DOCUMENT document, int page_index, ref byte buffer,
                uint buflen);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDF_CreateNewDocument")]
            public static extern FPDF_DOCUMENT FPDF_CreateNewDocument();

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFPage_New")]
            public static extern FPDF_PAGE FPDFPage_New(FPDF_DOCUMENT document, int page_index, double width,
                double height);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFPage_Delete")]
            public static extern void FPDFPage_Delete(FPDF_DOCUMENT document, int page_index);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFPage_GetRotation")]
            public static extern PageOrientations FPDFPage_GetRotation(FPDF_PAGE page);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFPage_SetRotation")]
            public static extern void FPDFPage_SetRotation(FPDF_PAGE page, PageOrientations rotation);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFPage_InsertObject")]
            public static extern void FPDFPage_InsertObject(FPDF_PAGE page, FPDF_PAGEOBJECT page_obj);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFPage_CountObject")]
            public static extern int FPDFPage_CountObject(FPDF_PAGE page);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFPage_GetObject")]
            public static extern FPDF_PAGEOBJECT FPDFPage_GetObject(FPDF_PAGE page, int index);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall,
                EntryPoint = "FPDFPage_HasTransparency")]
            public static extern bool FPDFPage_HasTransparency(FPDF_PAGE page);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall,
                EntryPoint = "FPDFPage_GenerateContent")]
            public static extern bool FPDFPage_GenerateContent(FPDF_PAGE page);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall,
                EntryPoint = "FPDFPageObj_HasTransparency")]
            public static extern bool FPDFPageObj_HasTransparency(FPDF_PAGEOBJECT pageObject);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFPageObj_Transform")]
            public static extern void FPDFPageObj_Transform(FPDF_PAGEOBJECT page_object, double a, double b, double c,
                double d, double e, double f);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall,
                EntryPoint = "FPDFPage_TransformAnnots")]
            public static extern void FPDFPage_TransformAnnots(FPDF_PAGE page, double a, double b, double c, double d,
                double e, double f);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFPageObj_NewImageObj")]
            public static extern FPDF_PAGEOBJECT FPDFPageObj_NewImageObj(FPDF_DOCUMENT document);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall,
                EntryPoint = "FPDFImageObj_LoadJpegFile")]
            public  static extern bool FPDFImageObj_LoadJpegFile(ref FPDF_PAGE pages, int nCount,
                FPDF_PAGEOBJECT image_object, FPDF_FILEREAD fileRead);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall,
                EntryPoint = "FPDFImageObj_LoadJpegFileInline")]
            public static extern bool FPDFImageObj_LoadJpegFileInline(ref FPDF_PAGE pages, int nCount,
                FPDF_PAGEOBJECT image_object, FPDF_FILEREAD fileRead);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFImageObj_SetMatrix")]
            public static extern bool FPDFImageObj_SetMatrix(FPDF_PAGEOBJECT image_object, double a, double b, double c,
                double d, double e, double f);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFImageObj_SetBitmap")]
            public static extern bool FPDFImageObj_SetBitmap(ref FPDF_PAGE pages, int nCount,
                FPDF_PAGEOBJECT image_object, FPDF_BITMAP bitmap);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall,
                EntryPoint = "FPDFPageObj_CreateNewPath")]
            public static extern FPDF_PAGEOBJECT FPDFPageObj_CreateNewPath(float x, float y);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall,
                EntryPoint = "FPDFPageObj_CreateNewRect")]
            public static extern FPDF_PAGEOBJECT FPDFPageObj_CreateNewRect(float x, float y, float w, float h);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFPath_SetStrokeColor")]
            public static extern bool FPDFPath_SetStrokeColor(FPDF_PAGEOBJECT path, uint R, uint G, uint B, uint A);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFPath_SetStrokeWidth")]
            public static extern bool FPDFPath_SetStrokeWidth(FPDF_PAGEOBJECT path, float width);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFPath_SetFillColor")]
            public static extern bool FPDFPath_SetFillColor(FPDF_PAGEOBJECT path, uint R, uint G, uint B, uint A);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFPath_MoveTo")]
            public static extern bool FPDFPath_MoveTo(FPDF_PAGEOBJECT path, float x, float y);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFPath_LineTo")]
            public static extern bool FPDFPath_LineTo(FPDF_PAGEOBJECT path, float x, float y);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFPath_BezierTo")]
            public static extern bool FPDFPath_BezierTo(FPDF_PAGEOBJECT path, float x1, float y1, float x2, float y2,
                float x3, float y3);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFPath_Close")]
            public static extern bool FPDFPath_Close(FPDF_PAGEOBJECT path);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFPath_SetDrawMode")]
            public static extern bool FPDFPath_SetDrawMode(FPDF_PAGEOBJECT path, PathFillModes fillmode, bool stroke);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFPageObj_NewTextObj")]
            public static extern FPDF_PAGEOBJECT FPDFPageObj_NewTextObj(FPDF_DOCUMENT document,
                [MarshalAs(UnmanagedType.LPStr)] string font, float font_size);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFText_SetText")]
            public static extern bool FPDFText_SetText(FPDF_PAGEOBJECT text_object,
                [MarshalAs(UnmanagedType.LPStr)] string text);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFText_LoadFont")]
            public static extern FPDF_FONT FPDFText_LoadFont(FPDF_DOCUMENT document, ref byte data, uint size,
                FontTypes font_type, bool cid);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFDoc_GetPageMode")]
            public static extern PageModes FPDFDoc_GetPageMode(FPDF_DOCUMENT document);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFPage_Flatten")]
            public static extern FlattenResults FPDFPage_Flatten(FPDF_PAGE page, FlattenFlags nFlag);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDF_ImportPages")]
            public static extern bool FPDF_ImportPages(FPDF_DOCUMENT dest_doc, FPDF_DOCUMENT src_doc,
                [MarshalAs(UnmanagedType.LPStr)] string pagerange, int index);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall,
                EntryPoint = "FPDF_CopyViewerPreferences")]
            public static extern bool FPDF_CopyViewerPreferences(FPDF_DOCUMENT dest_doc, FPDF_DOCUMENT src_doc);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall,
                EntryPoint = "FPDF_RenderPageBitmap_Start")]
            public static extern RenderingStatus FPDF_RenderPageBitmap_Start(FPDF_BITMAP bitmap, FPDF_PAGE page,
                int start_x, int start_y, int size_x, int size_y, PageOrientations rotate, RenderingFlags flags,
                IFSDK_PAUSE pause);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall,
                EntryPoint = "FPDF_RenderPage_Continue")]
            public static extern RenderingStatus FPDF_RenderPage_Continue(FPDF_PAGE page, IFSDK_PAUSE pause);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDF_RenderPage_Close")]
            public static extern void FPDF_RenderPage_Close(FPDF_PAGE page);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDF_SaveAsCopy")]
            public static extern bool FPDF_SaveAsCopy(FPDF_DOCUMENT document, FPDF_FILEWRITE fileWrite,
                SaveFlags flags);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDF_SaveWithVersion")]
            public static extern bool FPDF_SaveWithVersion(FPDF_DOCUMENT document, FPDF_FILEWRITE fileWrite,
                SaveFlags flags, int fileVersion);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall,
                EntryPoint = "FPDFText_GetCharIndexFromTextIndex")]
            public static extern int FPDFText_GetCharIndexFromTextIndex(FPDF_TEXTPAGE text_page, int nTextIndex);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall,
                EntryPoint = "FPDFText_GetTextIndexFromCharIndex")]
            public static extern int FPDFText_GetTextIndexFromCharIndex(FPDF_TEXTPAGE text_page, int nCharIndex);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall,
                EntryPoint = "FPDF_StructTree_GetForPage")]
            public static extern FPDF_STRUCTTREE FPDF_StructTree_GetForPage(FPDF_PAGE page);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDF_StructTree_Close")]
            public static extern void FPDF_StructTree_Close(FPDF_STRUCTTREE struct_tree);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall,
                EntryPoint = "FPDF_StructTree_CountChildren")]
            public static extern int FPDF_StructTree_CountChildren(FPDF_STRUCTTREE struct_tree);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall,
                EntryPoint = "FPDF_StructTree_GetChildAtIndex")]
            public static extern FPDF_STRUCTELEMENT FPDF_StructTree_GetChildAtIndex(FPDF_STRUCTTREE struct_tree,
                int index);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall,
                EntryPoint = "FPDF_StructElement_GetAltText")]
            public static extern uint FPDF_StructElement_GetAltText(FPDF_STRUCTELEMENT struct_element, ref byte buffer,
                uint buflen);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall,
                EntryPoint = "FPDF_StructElement_CountChildren")]
            public static extern int FPDF_StructElement_CountChildren(FPDF_STRUCTELEMENT struct_element);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall,
                EntryPoint = "FPDF_StructElement_GetChildAtIndex")]
            public static extern FPDF_STRUCTELEMENT FPDF_StructElement_GetChildAtIndex(
                FPDF_STRUCTELEMENT struct_element, int index);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFText_LoadPage")]
            public static extern FPDF_TEXTPAGE FPDFText_LoadPage(FPDF_PAGE page);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFText_ClosePage")]
            public static extern void FPDFText_ClosePage(FPDF_TEXTPAGE text_page);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFText_CountChars")]
            public static extern int FPDFText_CountChars(FPDF_TEXTPAGE text_page);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFText_GetUnicode")]
            [return: MarshalAs(UnmanagedType.U4)]
            public static extern char FPDFText_GetUnicode(FPDF_TEXTPAGE text_page, int index);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFText_GetFontSize")]
            public static extern double FPDFText_GetFontSize(FPDF_TEXTPAGE text_page, int index);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFText_GetCharBox")]
            public static extern void FPDFText_GetCharBox(FPDF_TEXTPAGE text_page, int index, out double left,
                out double right, out double bottom, out double top);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall,
                EntryPoint = "FPDFText_GetCharIndexAtPos")]
            public static extern int FPDFText_GetCharIndexAtPos(FPDF_TEXTPAGE text_page, double x, double y,
                double xTolerance, double yTolerance);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFText_GetText")]
            public  static extern int FPDFText_GetText(FPDF_TEXTPAGE text_page, int start_index, int count,
                ref byte result);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFText_CountRects")]
            public static extern int FPDFText_CountRects(FPDF_TEXTPAGE text_page, int start_index, int count);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFText_GetRect")]
            public static extern void FPDFText_GetRect(FPDF_TEXTPAGE text_page, int rect_index, out double left,
                out double top, out double right, out double bottom);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFText_GetBoundedText")]
            public static extern int FPDFText_GetBoundedText(FPDF_TEXTPAGE text_page, double left, double top,
                double right, double bottom, ref byte buffer, int buflen);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFText_FindStart")]
            public static extern FPDF_SCHHANDLE FPDFText_FindStart(FPDF_TEXTPAGE text_page,
                [MarshalAs(UnmanagedType.LPWStr)] string findwhat, SearchFlags flags, int start_index);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFText_FindNext")]
            public static extern bool FPDFText_FindNext(FPDF_SCHHANDLE handle);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFText_FindPrev")]
            public static extern bool FPDFText_FindPrev(FPDF_SCHHANDLE handle);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall,
                EntryPoint = "FPDFText_GetSchResultIndex")]
            public static extern int FPDFText_GetSchResultIndex(FPDF_SCHHANDLE handle);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFText_GetSchCount")]
            public static extern int FPDFText_GetSchCount(FPDF_SCHHANDLE handle);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFText_FindClose")]
            public static extern void FPDFText_FindClose(FPDF_SCHHANDLE handle);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFLink_LoadWebLinks")]
            public static extern FPDF_PAGELINK FPDFLink_LoadWebLinks(FPDF_TEXTPAGE text_page);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFLink_CountWebLinks")]
            public static extern int FPDFLink_CountWebLinks(FPDF_PAGELINK link_page);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFLink_GetURL")]
            public static extern int FPDFLink_GetURL(FPDF_PAGELINK link_page, int link_index, ref byte buffer,
                int buflen);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFLink_CountRects")]
            public static extern int FPDFLink_CountRects(FPDF_PAGELINK link_page, int link_index);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFLink_GetRect")]
            public static extern void FPDFLink_GetRect(FPDF_PAGELINK link_page, int link_index, int rect_index,
                out double left, out double top, out double right, out double bottom);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFLink_CloseWebLinks")]
            public static extern void FPDFLink_CloseWebLinks(FPDF_PAGELINK link_page);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFPage_SetMediaBox")]
            public static extern void FPDFPage_SetMediaBox(FPDF_PAGE page, float left, float bottom, float right,
                float top);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFPage_SetCropBox")]
            public static extern void FPDFPage_SetCropBox(FPDF_PAGE page, float left, float bottom, float right,
                float top);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFPage_GetMediaBox")]
            public static extern bool FPDFPage_GetMediaBox(FPDF_PAGE page, out float left, out float bottom,
                out float right, out float top);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFPage_GetCropBox")]
            public static extern bool FPDFPage_GetCropBox(FPDF_PAGE page, out float left, out float bottom,
                out float right, out float top);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall,
                EntryPoint = "FPDFPage_TransFormWithClip")]
            public static extern bool FPDFPage_TransFormWithClip(FPDF_PAGE page,
                [MarshalAs(UnmanagedType.LPStruct)] FS_MATRIX matrix,
                [MarshalAs(UnmanagedType.LPStruct)] FS_RECTF clipRect);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall,
                EntryPoint = "FPDFPageObj_TransformClipPath")]
            public static extern void FPDFPageObj_TransformClipPath(FPDF_PAGEOBJECT page_object, double a, double b,
                double c, double d, double e, double f);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDF_CreateClipPath")]
            public static extern FPDF_CLIPPATH FPDF_CreateClipPath(float left, float bottom, float right, float top);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDF_DestroyClipPath")]
            public static extern void FPDF_DestroyClipPath(FPDF_CLIPPATH clipPath);

            [DllImport("pdfium", CallingConvention = CallingConvention.StdCall, EntryPoint = "FPDFPage_InsertClipPath")]
            public static extern void FPDFPage_InsertClipPath(FPDF_PAGE page, FPDF_CLIPPATH clipPath);
        }
    }
}
