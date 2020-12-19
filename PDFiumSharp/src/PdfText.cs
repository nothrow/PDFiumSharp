using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using PDFiumSharp.Types;

namespace PDFiumSharp
{
    public sealed partial class PdfText : NativeWrapper<FPDF_TEXTPAGE>
    {
        // Private constructor. See static Load method below.
        PdfText(PdfPage page, FPDF_TEXTPAGE text) : base(text)
        {
            if (text.IsNull) throw new PDFiumException();
            Page = page;
        }

        public static PdfText Load(PdfPage page) => new PdfText(page, PDFium.FPDFText_LoadPage(page.Handle));

        public PdfPage Page { get; }

        protected override void Dispose(FPDF_TEXTPAGE handle)
        {
            PDFium.FPDFText_ClosePage(handle);
        }

        public int CountChars() => PDFium.FPDFText_CountChars(Handle);

        /// <summary>
        /// Returns all the text on the page.
        /// </summary>
        /// <returns></returns>
        public string GetText() => PDFium.FPDFText_GetText(Handle, 0, CountChars());

        public string GetText(int start_index, int count) => PDFium.FPDFText_GetText(Handle, start_index, count);

        public string GetBoundedText(double left, double top, double right, double bottom) => PDFium.FPDFText_GetBoundedText(Handle, left, top, right, bottom);

        private List<PdfTextInfo> GetBoundedTextInfo(double left, double top, double right, double bottom)
        {
            var boundedArea = new FS_RECTF((float)left, (float)top, (float)right, (float)bottom);
            var textInfoList = new List<PdfTextInfo>();            
            var allRects = GetAllRects();

            GetAllCharInfo(); //Ensure char info is cached in memory

            foreach (var rect in allRects)
            {
                if (rect.IntersectsWith(boundedArea))
                {
                    // Find all the characters that fit in this rect AND the boundedArea
                    var chars = _allCharInfo.FindAll(c => rect.Contains(c.BoundingRectangle) && boundedArea.ContainsPartially(c.BoundingRectangle, 50));
                    var charBoxes = new List<FS_RECTF>();
                    chars.ForEach(c => charBoxes.Add(c.BoundingRectangle));
                    var boundingRect = FS_RECTF.Union(charBoxes);

                    textInfoList.Add(new PdfTextInfo(string.Join<PdfTextInfo>(string.Empty, chars.ToArray()), -1, chars.Count, boundingRect, charBoxes));
                }
            }

            textInfoList.Sort((x, y) =>
            {
                // Sort top to bottom, left to right.
                int retval = -x.BoundingRectangle.Bottom.CompareTo(y.BoundingRectangle.Bottom);
                return retval == 0 ? x.BoundingRectangle.Left.CompareTo(y.BoundingRectangle.Left) : retval;                
            });

            return textInfoList;
        }

        public List<PdfTextBlock> GetSegmentedText() => PartitionIntoBlocks((float)Page.Width, GetBoundedTextInfo(0, Page.Height, Page.Width, 0));

        private static bool IsSimilar(float x1, float x2, float tolerance)
        {
            return Math.Abs(x1 - x2) < tolerance;
        }

        private static float DistanceBetween((float x, float y) from, (float x, float y) to)
        {
            var dx = from.x - to.x;
            var dy = from.y - to.y;
            return MathF.Sqrt(dx * dx + dy * dy);
        }

        private static List<PdfTextBlock> PartitionIntoBlocks(float pageWidth, IEnumerable<PdfTextInfo> inputLines)
        {
            var lines = new List<PdfTextInfo>(inputLines);
            var blocks = new List<PdfTextBlock>();

            if (!lines.Any()) return blocks;

            var leftTol  = 0.06f * pageWidth;
            var distTol  = 0.06f * pageWidth;
            var widthTol = 0.4f * pageWidth;

            while (lines.Any())
            {
                var block = new PdfTextBlock();
                var targetLine = lines.First();
                block.AddLine(targetLine);
                var sameLeft = targetLine.BoundingRectangle.Left;

                foreach (var line in lines.Skip(1))
                {
                    var actualWidthTol = MathF.Max(widthTol, 0.5f * Math.Abs(targetLine.BoundingRectangle.Width - line.BoundingRectangle.Width));

                    if(IsSimilar(sameLeft, line.BoundingRectangle.Left, leftTol) &&
                        (IsSimilar(targetLine.BoundingRectangle.Width, line.BoundingRectangle.Width, actualWidthTol) || 
                         DistanceBetween(block.Lines.Last().BoundingRectangle.GetCenter(), line.BoundingRectangle.GetCenter()) < distTol))
                    {
                        block.AddLine(line);
                    }
                }
                
                block.Lines.ForEach(l => lines.Remove(l));

                blocks.Add(block);
            }

            return SortBlocks(blocks);
        }


        private static List<PdfTextBlock> SortBlocks(List<PdfTextBlock> blocks)
        {
            if (!blocks.Any()) return blocks;
            
            var sortedBlocks = new List<PdfTextBlock>();
            var toProcess    = new HashSet<PdfTextBlock>(blocks);
            sortedBlocks.Add(blocks.First());
            toProcess.Remove(blocks.First());
            
            while (toProcess.Any())
            {
                var mostLeft = MostLeftBlock(toProcess);
                sortedBlocks.Add(mostLeft);
                toProcess.Remove(mostLeft);
            }

            return sortedBlocks;
        }

        private static PdfTextBlock MostLeftBlock(IEnumerable<PdfTextBlock> blocks)
        {
            return blocks.OrderBy(b => b.BoundingRectangle.Left).First();
        }

        public char GetCharacter(int index)
        {
            return PDFium.FPDFText_GetUnicode(Handle, index);
        }

        public int GetCharIndexAtPos(double x, double y, double xTolerance, double yTolerance)
        {
            return PDFium.FPDFText_GetCharIndexAtPos(Handle, x, y, xTolerance, yTolerance);
        }

        public FS_RECTF GetCharBox(int index)
        {
            PDFium.FPDFText_GetCharBox(Handle, index, out var left, out var right, out var bottom, out var top);
            return new FS_RECTF((float)left, (float)top, (float)right, (float)bottom); 
        }

        private List<FS_RECTF> GetCharBoxes(int char_index, int char_count)
        {
            var charBoxes = new List<FS_RECTF>();
            
            for (int i = char_index; i < char_index + char_count; i++)
            {
                charBoxes.Add(GetCharBox(i));
            }
            return charBoxes;
        }

        public ReadOnlyCollection<PdfTextInfo> GetAllCharInfo(bool refresh = false)
        {
            if (_allCharInfo == null || refresh)
            {
                var allText = GetText(0, CountChars());
                var allCharInfo = new List<PdfTextInfo>();
                int charCount = CountChars();
                for (int i = 0; i < charCount; i++)
                {
                    var charBox = GetCharBox(i);
                    allCharInfo.Add(new PdfTextInfo(allText.Substring(i, 1), i, 1, charBox, new List<FS_RECTF>() { charBox } ));
                }
                _allCharInfo = allCharInfo;
            }
            return new ReadOnlyCollection<PdfTextInfo>(_allCharInfo);
        }

        private List<PdfTextInfo> _allCharInfo;

        public FS_RECTF GetRect(int rect_index)
        {
            PDFium.FPDFText_GetRect(Handle, rect_index, out var left, out var top, out var right, out var bottom);
            return new FS_RECTF((float)left, (float)top, (float)right, (float)bottom);
        }

        public FS_RECTF GetRect(int char_index, int char_count)
        {
            return GetRect(char_index, char_count, out _);
        }

        public FS_RECTF GetRect(int char_index, int char_count, out List<FS_RECTF> charBoxes)
        {
            charBoxes = GetCharBoxes(char_index, char_count);
            return FS_RECTF.Union(charBoxes);
        }
                
        public List<FS_RECTF> GetAllRects()
        {
            var rects = new List<FS_RECTF>();
            int rectCount = PDFium.FPDFText_CountRects(Handle, 0, CountChars());
            for (int i = 0; i < rectCount; i++)
            {
                rects.Add(GetRect(i));
            }
            return rects;
        }

        public float GetFontSize(int index)
        {
            return (float)PDFium.FPDFText_GetFontSize(Handle, index);
        }

        public int FindText(string searchText, SearchFlags searchFlags, int start_index = 0)
        {
            var hSearch = PDFium.FPDFText_FindStart(Handle, searchText, searchFlags, start_index);
            try
            {
                int countCharsFound = PDFium.FPDFText_GetSchCount(hSearch);
                int charIndex = PDFium.FPDFText_GetSchResultIndex(hSearch);
                return charIndex;
            }
            finally
            {
                PDFium.FPDFText_FindClose(hSearch);
            }
        }

        public List<PdfTextInfo> FindTextAll(string searchText, SearchFlags searchFlags, int start_index = 0)
        {
            var results = new List<PdfTextInfo>();
            var hSearch = PDFium.FPDFText_FindStart(Handle, searchText, searchFlags, start_index);
            
            try
            {
                while (PDFium.FPDFText_FindNext(hSearch))
                {
                    int countCharsFound = PDFium.FPDFText_GetSchCount(hSearch);
                    int charIndex = PDFium.FPDFText_GetSchResultIndex(hSearch);

                    var boundingRect = GetRect(charIndex, countCharsFound, out var charBoxes);

                    results.Add(new PdfTextInfo(GetText(charIndex, countCharsFound), charIndex, countCharsFound, boundingRect, charBoxes));
                }
            }
            finally
            {
                PDFium.FPDFText_FindClose(hSearch);
            }
            return results;
        }
    }
}
